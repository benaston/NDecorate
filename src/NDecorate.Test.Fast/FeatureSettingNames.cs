namespace NDecorate.Test.Fast
{
    using NFeature;

    public static class FeatureSettingNames
    {
        public enum NDecorate
        {
            ImplicitDecoratorSuffix,
            DecoratorListDelimiter,
            [FeatureSetting(FullName = "NDecorate.Test.Fast.MyQueryType1, NDecorate.Test.Fast")]
            MyQueryType1,
            [FeatureSetting(FullName = "NDecorate.Test.Fast.MyQueryType2, NDecorate.Test.Fast")]
            MyQueryType2,
        }
    }
}