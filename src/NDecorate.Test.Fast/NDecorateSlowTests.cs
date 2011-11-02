// ReSharper disable InconsistentNaming
namespace NDecorate.Test.Fast
{
    using System;
    using NFeature;
    using NFeature.Configuration;
    using NFeature.DefaultImplementations;
    using NUnit.Framework;

    [TestFixture]
    [Category("Slow")]
    public class SlowTests
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
            return Enum.GetName(typeof (Feature), s.Feature) == "NDecorate";
        }

        [Test]
        public void IsAvailable_WhenTheAvailabilityCheckingFunctionReturnsTrueAndDependenciesAreOK_ReturnsTrue()
        {
            Assert.That(Feature.NDecorate.IsAvailable(_featureManifest));
        }

        [Test]
        public void Setting_Returned_OK()
        {
            Assert.That(Feature.NDecorate.Setting(FeatureSettingNames.NDecorate.MyQueryType1, _featureManifest) == "Cache,Log");
        }
    }
}
// ReSharper restore InconsistentNaming