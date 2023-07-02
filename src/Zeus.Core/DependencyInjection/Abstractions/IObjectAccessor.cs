namespace Zeus.Core.DependencyInjection.Abstractions
{
    public interface IObjectAccessor<out T>
    {
        T? Value { get; }
    }
}
