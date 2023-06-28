using System.Diagnostics.CodeAnalysis;
using Zeus.Core.Application.Abstraction;

namespace Zeus.Core.Application.Modules.Abstractions
{
    public interface IOnPreApplicationInitialization
    {
        Task OnPreApplicationInitializationAsync([NotNull] IApplicationInitializationContext context);

        void OnPreApplicationInitialization([NotNull] IApplicationInitializationContext context);
    }
}
