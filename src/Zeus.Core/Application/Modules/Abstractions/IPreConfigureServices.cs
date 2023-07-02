namespace Zeus.Core.Application.Modules.Abstractions
{
    public interface IPreConfigureServices
    {
        Task PreConfigureServicesAsync(IApplicationServiceConfiguration context);

        void PreConfigureServices(IApplicationServiceConfiguration context);
    }
}
