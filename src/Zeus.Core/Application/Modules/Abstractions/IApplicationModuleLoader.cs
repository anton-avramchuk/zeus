using Microsoft.Extensions.DependencyInjection;

namespace Zeus.Core.Application.Modules.Abstractions
{
    public interface IApplicationModuleLoader
    {
        IApplicationModuleDescriptor[] LoadModules(
            IServiceCollection services,
            Type startupModuleType
        );
    }
}
