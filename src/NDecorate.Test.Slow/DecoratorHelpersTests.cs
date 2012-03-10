// Copyright 2012, Ben Aston (ben@bj.ma).
// 
// This file is part of NDecorate.
// 
// NDecorate is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// NDecorate is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with NDecorate. If not, see <http://www.gnu.org/licenses/>.

namespace NDecorate.Test.Fast
{
	using System;
	using System.Linq;
	using Moq;
	using NFeature;
	using NFeature.Configuration;
	using NFeature.DefaultImplementations;
	using NServiceLocator;
	using NUnit.Framework;
	using Newtonsoft.Json.Linq;
	using Ninject.Activation;

	[TestFixture]
	[Category("Slow")]
	public class DecoratorHelpersTests
	{
		#region Setup/Teardown

		[SetUp]
		public void Setup() {
			var availabilityChecker =
				new FeatureSettingAvailabilityChecker<Feature, EmptyArgs, DefaultTenantEnum>(MyAvailabilityCheckFunction);
			var featureSettingRepo = new AppConfigFeatureSettingRepository<Feature, DefaultTenantEnum>();
			var featureSettingService =
				new FeatureSettingService<Feature, DefaultTenantEnum, EmptyArgs>(availabilityChecker, featureSettingRepo);
			var manifestCreationStrategy =
				new ManifestCreationStrategyDefault<Feature, DefaultTenantEnum>(featureSettingRepo,
				                                                                featureSettingService);
			var featureManifestService = new FeatureManifestService<Feature>(manifestCreationStrategy);
			_featureManifest = featureManifestService.GetManifest();
		}

		#endregion

		private static T CastFromAnonType<T>(T entity, Object o) {
			return (T) o;
		}

		private IFeatureManifest<Feature> _featureManifest;

		/// <summary>
		/// 	A function to test the availability checking behavior.
		/// </summary>
		private static bool MyAvailabilityCheckFunction(FeatureSetting<Feature, DefaultTenantEnum> s, EmptyArgs args) {
			Func<bool> f = () => true;
			return Enum.GetName(typeof (Feature), s.Feature) == "NDecorate";
		}

		/// <summary>
		/// 	This test is a WIP.
		/// </summary>
		[Test]
		public void GetDecoratorsFor_WhenInvokedHappyPath_ReturnsCorrectDecoratorInstances() {
			var query = new MyAQuery1();

			Assert.That(query.Execute() == "hello");

			var serviceLocator = new Mock<IServiceLocator<IContext>>();
			serviceLocator.Setup(a => a.Locate(typeof (LogDecorator))).Returns(new LogDecorator());
			serviceLocator.Setup(a => a.Locate(typeof (CacheDecorator))).Returns(new CacheDecorator());

			var decoratorAliases = ((JArray) Feature.NDecorate.Setting(FeatureSettingNames.NDecorate.MyQuery1, _featureManifest)).Select(i => i.Value<string>());
			var decoratorAliasFullNames =
				((JArray) Feature.NDecorate.Setting(FeatureSettingNames.NDecorate.DecoratorTypeAliases, _featureManifest)).Select(i => i.Value<dynamic>());
			var decoratorTypeNames = decoratorAliases.Select(a => decoratorAliasFullNames.First(fn => fn.Alias == a).Type).Select(i => (string) i.Value);
			var decorators = DecoratorHelpers.GetDecoratorsFor<IQueryTypeA, IContext>(serviceLocator.Object, decoratorTypeNames);
			Assert.That(decorators.Length == 2);
			var decoratedQuery = query.Decorate(decorators);
			Assert.That(decoratedQuery.Execute() == "hello cached logged");
		}
	}

	public class LogDecorator : IQueryTypeA
	{
		public IQueryTypeA DecoratorTarget { get; set; }

		public string Execute() {
			return DecoratorTarget.Execute() + " logged";
		}
	}

	public class CacheDecorator : IQueryTypeA
	{
		public IQueryTypeA DecoratorTarget { get; set; }

		public string Execute() {
			return DecoratorTarget.Execute() + " cached";
		}
	}
}

// ReSharper restore InconsistentNaming