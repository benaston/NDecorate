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

namespace NDecorate.Test.Slow
{
	using System;
	using System.Linq;
	using Fast;
	using NFeature;
	using NFeature.Configuration;
	using NFeature.DefaultImplementations;
	using NUnit.Framework;
	using Newtonsoft.Json.Linq;

	[TestFixture]
	[Category("Slow")]
	public class SlowTests
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

		private IFeatureManifest<Feature> _featureManifest;

		/// <summary>
		/// 	A function to test the availability checking behavior.
		/// </summary>
		private static bool MyAvailabilityCheckFunction(FeatureSetting<Feature, DefaultTenantEnum> s, EmptyArgs args) {
			Func<bool> f = () => true;
			return Enum.GetName(typeof (Feature), s.Feature) == "NDecorate";
		}

		[Test]
		public void IsAvailable_WhenTheAvailabilityCheckingFunctionReturnsTrueAndDependenciesAreOK_ReturnsTrue() {
			Assert.That(Feature.NDecorate.IsAvailable(_featureManifest));
		}

		[Test]
		public void Setting_Returned_OK() {
			Assert.That(((JArray) Feature.NDecorate.Setting(FeatureSettingNames.NDecorate.MyQuery1, _featureManifest)).Select(i => i.Value<string>()).Contains("Cache"));
			Assert.That(((JArray) Feature.NDecorate.Setting(FeatureSettingNames.NDecorate.MyQuery1, _featureManifest)).Select(i => i.Value<string>()).Contains("Log"));
		}
	}
}

// ReSharper restore InconsistentNaming