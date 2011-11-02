// ReSharper disable InconsistentNaming
namespace NDecorate.Test.Fast
{
    using System;
    using NFeature;
    using NFeature.Configuration;
    using NFeature.DefaultImplementations;
    using Ninject.Modules;
    using NUnit.Framework;

    [TestFixture]
    public class Decorate_HappyPath_DecoratesType
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
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
        ///   A function to test the availability checking behavior.
        /// </summary>
        private static bool MyAvailabilityCheckFunction(FeatureSetting<Feature, DefaultTenantEnum> s, EmptyArgs args)
        {
            Func<bool> f = () => true;
            return Enum.GetName(typeof (Feature), s.Feature) == "TestFeatureE";
        }

        public class MyModule : NinjectModule
        {
            public override void Load()
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void TestMethod()
        {
            var query = new MyQuery1();
            Assert.That(query.Execute() == "hello");
            var decoratedQuery = query.Decorate(new IQueryTypeA[]
                                                    {
                                                        new WorldAdderDecorator(),
                                                        new ExclamationAdderDecorator(),
                                                    });
            Assert.That(decoratedQuery.Execute() == "hello world!");
        }
    }

    public interface IQueryTypeA : IDecorator<IQueryTypeA>
    {
        string Execute();
    }

    public class MyQuery1 : IQueryTypeA
    {
        public string Execute()
        {
            return "hello";
        }

        public IQueryTypeA DecoratorTarget { get; set; }
    }

    public class MyQuery2 : IQueryTypeA
    {
        public string Execute()
        {
            return "goodbye";
        }

        public IQueryTypeA DecoratorTarget { get; set; }
    }

    public class WorldAdderDecorator : IQueryTypeA
    {
        public IQueryTypeA DecoratorTarget { get; set; }

        public string Execute()
        {
            return DecoratorTarget.Execute() + " world";
        }
    }

    public class ExclamationAdderDecorator : IQueryTypeA
    {
        public IQueryTypeA DecoratorTarget { get; set; }

        public string Execute()
        {
            return DecoratorTarget.Execute() + "!";
        }
    }
}

// ReSharper restore InconsistentNaming