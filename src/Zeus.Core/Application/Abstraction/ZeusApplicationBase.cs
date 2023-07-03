using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Zeus.Core.Application.Modules.Abstractions;
using Zeus.Core.Application.Modules;
using Zeus.Core.DependencyInjection;
using Zeus.Core.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zeus.Core.Logs.Abstractions;
using Microsoft.Extensions.Hosting;
using Zeus.Core.Application.Exceptions;

namespace Zeus.Core.Application.Abstraction
{
    public abstract class ZeusApplicationBase : IZeusApplication
    {
        [NotNull]
        public Type StartupModuleType { get; }

        public IServiceProvider ServiceProvider { get; private set; } = default!;

        public IServiceCollection Services { get; }

        public IReadOnlyList<IApplicationModuleDescriptor> Modules { get; }

        public string? ApplicationName { get; }

        public string InstanceId { get; } = Guid.NewGuid().ToString();

        private bool _configuredServices;

        internal ZeusApplicationBase(
            Type startupModuleType,
            IServiceCollection services,
            Action<ZeusApplicationCreationOptions>? optionsAction)
        {
            StartupModuleType = startupModuleType;
            Services = services;

            services.TryAddObjectAccessor<IServiceProvider>();

            var options = new ZeusApplicationCreationOptions(services);
            optionsAction?.Invoke(options);

            ApplicationName = GetApplicationName(options);

            services.AddSingleton<IZeusApplication>(this);
            services.AddSingleton<IApplicationInfoAccessor>(this);
            services.AddSingleton<IApplicationModuleContainer>(this);
            services.AddSingleton<IZeusHostEnvironment>(new ZeusHostEnvironment()
            {
                EnvironmentName = options.Environment
            });

            services.AddCoreServices();
            services.AddCoreAbpServices(this, options);

            Modules = LoadModules(services, options);

            if (!options.SkipConfigureServices)
            {
                ConfigureServices();
            }
        }

        public virtual async Task ShutdownAsync()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                await scope.ServiceProvider
                    .GetRequiredService<IApplicationModuleManager>()
                    .ShutdownModulesAsync(new ApplicationShutdownContext(scope.ServiceProvider));
            }
        }

        public virtual void Shutdown()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                scope.ServiceProvider
                    .GetRequiredService<IApplicationModuleManager>()
                    .ShutdownModules(new ApplicationShutdownContext(scope.ServiceProvider));
            }
        }

        public virtual void Dispose()
        {
            //TODO: Shutdown if not done before?
        }

        protected virtual void SetServiceProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            ServiceProvider.GetRequiredService<ObjectAccessor<IServiceProvider>>().Value = ServiceProvider;
        }

        protected virtual async Task InitializeModulesAsync()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                WriteInitLogs(scope.ServiceProvider);
                await scope.ServiceProvider
                    .GetRequiredService<IApplicationModuleManager>()
                    .InitializeModulesAsync(new ApplicationInitializationContext(scope.ServiceProvider));
            }
        }

        protected virtual void InitializeModules()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                WriteInitLogs(scope.ServiceProvider);
                scope.ServiceProvider
                    .GetRequiredService<IApplicationModuleManager>()
                    .InitializeModules(new ApplicationInitializationContext(scope.ServiceProvider));
            }
        }

        protected virtual void WriteInitLogs(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetService<ILogger<ZeusApplicationBase>>();
            if (logger == null)
            {
                return;
            }

            var initLogger = serviceProvider.GetRequiredService<IInitLoggerFactory>().Create<ZeusApplicationBase>();

            foreach (var entry in initLogger.Entries)
            {
                logger.Log(entry.LogLevel, entry.EventId, entry.State, entry.Exception, entry.Formatter);
            }

            initLogger.Entries.Clear();
        }

        protected virtual IReadOnlyList<IApplicationModuleDescriptor> LoadModules(IServiceCollection services, ZeusApplicationCreationOptions options)
        {
            return services
                .GetSingletonInstance<IApplicationModuleLoader>()
                .LoadModules(
                    services,
                    StartupModuleType
                );
        }

        //TODO: We can extract a new class for this
        public virtual async Task ConfigureServicesAsync()
        {
            CheckMultipleConfigureServices();

            var context = new ServiceConfigurationContext(Services);
            Services.AddSingleton(context);

            foreach (var module in Modules)
            {
                if (module.Instance is AbpModule abpModule)
                {
                    abpModule.ServiceConfigurationContext = context;
                }
            }

            //PreConfigureServices
            foreach (var module in Modules.Where(m => m.Instance is IPreConfigureServices))
            {
                try
                {
                    await ((IPreConfigureServices)module.Instance).PreConfigureServicesAsync(context);
                }
                catch (Exception ex)
                {
                    throw new AbpInitializationException($"An error occurred during {nameof(IPreConfigureServices.PreConfigureServicesAsync)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
                }
            }

            var assemblies = new HashSet<Assembly>();

            //ConfigureServices
            foreach (var module in Modules)
            {
                if (module.Instance is AbpModule abpModule)
                {
                    if (!abpModule.SkipAutoServiceRegistration)
                    {
                        var assembly = module.Type.Assembly;
                        if (!assemblies.Contains(assembly))
                        {
                            Services.AddAssembly(assembly);
                            assemblies.Add(assembly);
                        }
                    }
                }

                try
                {
                    await module.Instance.ConfigureServicesAsync(context);
                }
                catch (Exception ex)
                {
                    throw new AbpInitializationException($"An error occurred during {nameof(IAbpModule.ConfigureServicesAsync)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
                }
            }

            //PostConfigureServices
            foreach (var module in Modules.Where(m => m.Instance is IPostConfigureServices))
            {
                try
                {
                    await ((IPostConfigureServices)module.Instance).PostConfigureServicesAsync(context);
                }
                catch (Exception ex)
                {
                    throw new AbpInitializationException($"An error occurred during {nameof(IPostConfigureServices.PostConfigureServicesAsync)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
                }
            }

            foreach (var module in Modules)
            {
                if (module.Instance is AbpModule abpModule)
                {
                    abpModule.ServiceConfigurationContext = null!;
                }
            }

            _configuredServices = true;

            TryToSetEnvironment(Services);
        }

        private void CheckMultipleConfigureServices()
        {
            if (_configuredServices)
            {
                throw new AbpInitializationException("Services have already been configured! If you call ConfigureServicesAsync method, you must have set AbpApplicationCreationOptions.SkipConfigureServices to true before.");
            }
        }

        //TODO: We can extract a new class for this
        public virtual void ConfigureServices()
        {
            CheckMultipleConfigureServices();

            var context = new ServiceConfigurationContext(Services);
            Services.AddSingleton(context);

            foreach (var module in Modules)
            {
                if (module.Instance is AbpModule abpModule)
                {
                    abpModule.ServiceConfigurationContext = context;
                }
            }

            //PreConfigureServices
            foreach (var module in Modules.Where(m => m.Instance is IPreConfigureServices))
            {
                try
                {
                    ((IPreConfigureServices)module.Instance).PreConfigureServices(context);
                }
                catch (Exception ex)
                {
                    throw new ZeusInitializationException($"An error occurred during {nameof(IPreConfigureServices.PreConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
                }
            }

            var assemblies = new HashSet<Assembly>();

            //ConfigureServices
            foreach (var module in Modules)
            {
                if (module.Instance is ApplicationModule applicationModule)
                {
                    //if (!applicationModule.SkipAutoServiceRegistration)
                    //{
                    //    var assembly = module.Type.Assembly;
                    //    if (!assemblies.Contains(assembly))
                    //    {
                    //        Services.AddAssembly(assembly);
                    //        assemblies.Add(assembly);
                    //    }
                    //}
                    var assembly = module.Type.Assembly;
                    if (!assemblies.Contains(assembly))
                    {
                        Services.AddAssembly(assembly);
                        assemblies.Add(assembly);
                    }
                }

                try
                {
                    module.Instance.ConfigureServices(context);
                }
                catch (Exception ex)
                {
                    throw new ZeusInitializationException($"An error occurred during {nameof(IApplicationModule.ConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
                }
            }

            //PostConfigureServices
            foreach (var module in Modules.Where(m => m.Instance is IPostConfigureServices))
            {
                try
                {
                    ((IPostConfigureServices)module.Instance).PostConfigureServices(context);
                }
                catch (Exception ex)
                {
                    throw new ZeusInitializationException($"An error occurred during {nameof(IPostConfigureServices.PostConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
                }
            }

            foreach (var module in Modules)
            {
                if (module.Instance is ApplicationModule applicationModule)
                {
                    applicationModule.ServiceConfigurationContext = null!;
                }
            }

            _configuredServices = true;

            TryToSetEnvironment(Services);
        }

        private static string? GetApplicationName(ZeusApplicationCreationOptions options)
        {
            if (!string.IsNullOrWhiteSpace(options.ApplicationName))
            {
                return options.ApplicationName!;
            }

            var configuration = options.Services.GetConfigurationOrNull();
            if (configuration != null)
            {
                var appNameConfig = configuration["ApplicationName"];
                if (!string.IsNullOrWhiteSpace(appNameConfig))
                {
                    return appNameConfig!;
                }
            }

            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                return entryAssembly.GetName().Name;
            }

            return null;
        }

        private static void TryToSetEnvironment(IServiceCollection services)
        {
            var hostEnvironment = services.GetSingletonInstance<IZeusHostEnvironment>();
            if (hostEnvironment != null && (hostEnvironment.EnvironmentName != null || string.IsNullOrEmpty(hostEnvironment.EnvironmentName)))
            {
                hostEnvironment.EnvironmentName = Environments.Production;
            }
        }
    }
}
