using Zeus.Core.Application.Abstraction;

namespace Zeus.Core.Application.Modules.Abstractions
{
    public interface IApplicationModuleManager
    {
        Task InitializeModulesAsync(IApplicationInitializationContext context);

        void InitializeModules(IApplicationInitializationContext context);

        Task ShutdownModulesAsync(IApplicationShutdownContext context);

        void ShutdownModules(IApplicationShutdownContext context);
    }
}
