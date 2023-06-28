namespace Zeus.Core.Application.DependencyInjection.Abstractions
{
    public interface IServiceProviderAccessor
    {
        IServiceProvider ServiceProvider { get; }
    }
}
