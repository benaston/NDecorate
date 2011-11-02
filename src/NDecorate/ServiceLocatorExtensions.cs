namespace NDecorate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NServiceLocator;

    public static class DecoratorHelpers
    {
        /// <summary>
        /// WIP
        /// </summary>
        public static TSharedInterface[] GetDecoratorsFor<TSharedInterface, TActivationContext>(IServiceLocator<TActivationContext> serviceLocator,
                                                                            string[] decoratorPrefixes,
                                                                            string implicitDecoratorSuffix)
            where TSharedInterface : IDecorateable<TSharedInterface>, IDecorator<TSharedInterface>
        {
            var decoratorTypeNames = decoratorPrefixes.Select(i => i + implicitDecoratorSuffix);
            var decorators = new List<TSharedInterface>();

            foreach (var decoratorTypeName in decoratorTypeNames)
            {
                decorators.Add((TSharedInterface)serviceLocator.Locate(Type.GetType(decoratorTypeName)));
            }

            return decorators.ToArray();
        }

        //public static string[] GetDecoratorPrefixesFor<TSharedInterface, TFeature>(Enum feature, 
        //                                                                           Enum decoratorSetting, 
        //                                                                           FeatureManifest<TFeature> featureManifest,
        //                                                                           string decoratorListDelimiter = ",")
        //    where TFeature : struct
        //{
        //    var decoratorPrefixes =
        //            feature.Setting(decoratorSetting, featureManifest).Split(
        //                new[] { decoratorListDelimiter },
        //                        StringSplitOptions.RemoveEmptyEntries);
        //}
    }
}