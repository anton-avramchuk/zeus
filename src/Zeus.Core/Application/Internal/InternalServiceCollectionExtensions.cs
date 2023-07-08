using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zeus.Core.Logging.Abstractions;
using Zeus.Core.Reflection.Abstractions;
using Zeus.Core.Reflection;
using Zeus.Core.Application.Abstraction;
using Zeus.Core.Application.Modules;
using Zeus.Core.Extensions.DependencyInjection;

namespace Zeus.Core.Application.Internal
{
    internal static class InternalServiceCollectionExtensions
    {
        internal static void AddCoreServices(this IServiceCollection services)
        {
            services.AddOptions();
            services.AddLogging();
            //services.AddLocalization();
        }

        internal static void AddCoreAbpServices(this IServiceCollection services,
            IZeusApplication abpApplication,
            ZeusApplicationCreationOptions applicationCreationOptions)
        {
            var moduleLoader = new ApplicationModuleLoader();
            var assemblyFinder = new AssemblyFinder(abpApplication);
            var typeFinder = new TypeFinder(assemblyFinder);

            if (!services.IsAdded<IConfiguration>())
            {
                services.ReplaceConfiguration(
                    ConfigurationHelper.BuildConfiguration(
                        applicationCreationOptions.Configuration
                    )
                );
            }

            services.TryAddSingleton<IModuleLoader>(moduleLoader);
            services.TryAddSingleton<IAssemblyFinder>(assemblyFinder);
            services.TryAddSingleton<ITypeFinder>(typeFinder);
            services.TryAddSingleton<IInitLoggerFactory>(new DefaultInitLoggerFactory());

            services.AddAssemblyOf<IAbpApplication>();

            services.AddTransient(typeof(ISimpleStateCheckerManager<>), typeof(SimpleStateCheckerManager<>));

            services.Configure<AbpModuleLifecycleOptions>(options =>
            {
                options.Contributors.Add<OnPreApplicationInitializationModuleLifecycleContributor>();
                options.Contributors.Add<OnApplicationInitializationModuleLifecycleContributor>();
                options.Contributors.Add<OnPostApplicationInitializationModuleLifecycleContributor>();
                options.Contributors.Add<OnApplicationShutdownModuleLifecycleContributor>();
            });
        }
    }
}
