using System.Diagnostics.CodeAnalysis;
using Zeus.Core.Application.Abstraction;

namespace Zeus.Core.Application
{
    public class ApplicationInitializationContext : IApplicationInitializationContext
    {
        public ApplicationInitializationContext([NotNull] IServiceProvider serviceProvider)
        {


            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }
    }
}
