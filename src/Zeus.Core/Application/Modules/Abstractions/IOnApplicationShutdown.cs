using System.Diagnostics.CodeAnalysis;

namespace Zeus.Core.Application.Modules.Abstractions
{
    public interface IOnApplicationShutdown
    {
        Task OnApplicationShutdownAsync([NotNull] IApplicationShutdownContext context);

        void OnApplicationShutdown([NotNull] IApplicationShutdownContext context);
    }
}
