using System.Diagnostics.CodeAnalysis;
using Zeus.Core.Application.Abstraction;

namespace Zeus.Core.Application.Modules.Abstractions
{
    public interface IOnPostApplicationInitialization
    {
        Task OnPostApplicationInitializationAsync([NotNull] IApplicationInitializationContext context);

        void OnPostApplicationInitialization([NotNull] IApplicationInitializationContext context);
    }
}
