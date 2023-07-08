using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zeus.Core.Logging.Abstractions;

namespace Zeus.Core.Extensions.DependencyInjection
{
    public static class ServiceCollectionLoggingExtensions
    {
        public static ILogger<T> GetInitLogger<T>(this IServiceCollection services)
        {
            return services.GetSingletonInstance<IInitLoggerFactory>().Create<T>();
        }
    }
}
