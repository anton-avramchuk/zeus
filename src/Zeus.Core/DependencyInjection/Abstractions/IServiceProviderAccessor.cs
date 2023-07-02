namespace Zeus.Core.DependencyInjection.Abstractions
{
    public interface IServiceProviderAccessor
    {
        IServiceProvider ServiceProvider { get; }
    }
}
