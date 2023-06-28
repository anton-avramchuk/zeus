using System.Diagnostics.CodeAnalysis;
using Zeus.Core.Application.Abstraction;

namespace Zeus.Core.Application.Modules.Abstractions
{
    public interface IOnApplicationInitialization
    {
        Task OnApplicationInitializationAsync([NotNull] IApplicationInitializationContext context);

        void OnApplicationInitialization([NotNull] IApplicationInitializationContext context);
    }
}
