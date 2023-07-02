using System.Diagnostics.CodeAnalysis;
using Zeus.Core.Application.Modules.Abstractions;

namespace Zeus.Core.Application.Modules
{
    public class ApplicationShutdownContext : IApplicationShutdownContext
    {
        public IServiceProvider ServiceProvider { get; }

        public ApplicationShutdownContext([NotNull] IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }

}
