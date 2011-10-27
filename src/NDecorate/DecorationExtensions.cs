// ReSharper disable InconsistentNaming
namespace NDecorate
{
    using System;

    //add method to decorate all types in the container
    //,once container constructed, then invoke decorate 
    //to replace the implementations?
    public static class IDecoratableExtensions
    {
        public static TSharedInterface
            Decorate<TSharedInterface>(this TSharedInterface instanceToDecorate,
                                       TSharedInterface[] decoratorList) //to be supplied via service locator
            where TSharedInterface : IDecorateable<TSharedInterface>, IDecorator<TSharedInterface>
        {
            for (var x = 0; x <= decoratorList.Length - 1; x++)
            {
                var decorator = decoratorList[x];
                var targetDecorateableInstance = x == 0
                                                     ? instanceToDecorate
                                                     : decoratorList[x - 1];

                decorator.DecoratorTarget = targetDecorateableInstance;

                if (x == decoratorList.Length - 1)
                {
                    return decorator;
                }
            }

            throw new Exception("Problem decorating type.");
        }
    }
}
// ReSharper restore InconsistentNaming