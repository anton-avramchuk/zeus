using System.Diagnostics.CodeAnalysis;
using Zeus.Core.Application.Abstraction;
using Zeus.Core.Application.DependencyInjection.Abstractions;

namespace Zeus.Core.Application.Modules.Abstractions
{
    public interface IOnPreApplicationInitialization: IServiceProviderAccessor
    {
        Task OnPreApplicationInitializationAsync([NotNull] IApplicationInitializationContext context);

        void OnPreApplicationInitialization([NotNull] IApplicationInitializationContext context);
    }
}
