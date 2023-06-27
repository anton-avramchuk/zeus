namespace Zeus.Core.Application.Modules.Abstractions
{
    public interface IApplicationModule
    {
        Task ConfigureServicesAsync(IApplicationServiceConfiguration config);

        void ConfigureServices(IApplicationServiceConfiguration config);
    }
}
