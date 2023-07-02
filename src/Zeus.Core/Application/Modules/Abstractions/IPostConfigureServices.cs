namespace Zeus.Core.Application.Modules.Abstractions
{
    public interface IPostConfigureServices
    {
        Task PostConfigureServicesAsync(IApplicationServiceConfiguration context);

        void PostConfigureServices(IApplicationServiceConfiguration context);
    }
}
