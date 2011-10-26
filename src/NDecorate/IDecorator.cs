namespace NDecorate
{
    /// <summary>
    ///   Decorators are themselves decorateable to permit chaining effect.
    /// </summary>
    public interface IDecorator<TSharedInterface> : IDecorateable<TSharedInterface>
    {
        TSharedInterface Target { get; set; }
    }
}