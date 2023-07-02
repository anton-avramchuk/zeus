using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Zeus.Core.Application.Abstraction;
using Zeus.Core.Application.Modules.Abstractions;
using Zeus.Core.Extensions.DependencyInjection;

namespace Zeus.Core.Application.Modules
{
    public abstract class ApplicationModule :
        IApplicationModule,
        IOnApplicationInitialization,
        IOnPreApplicationInitialization,
        IOnPostApplicationInitialization,
        IOnApplicationShutdown,
    IPreConfigureServices,
    IPostConfigureServices
    {

        protected internal IApplicationServiceConfiguration ServiceConfigurationContext
        {
            get
            {
                if (_serviceConfigurationContext == null)
                {
                    throw new Exception($"{nameof(ServiceConfigurationContext)} is only available in the {nameof(ConfigureServices)}, {nameof(PreConfigureServices)} and {nameof(PostConfigureServices)} methods.");
                }

                return _serviceConfigurationContext;
            }
            internal set => _serviceConfigurationContext = value;
        }

        private IApplicationServiceConfiguration? _serviceConfigurationContext;


        public virtual void ConfigureServices(IApplicationServiceConfiguration config)
        {

        }

        public Task ConfigureServicesAsync(IApplicationServiceConfiguration config)
        {
            ConfigureServices(config);
            return Task.CompletedTask;
        }

        public static bool IsApplicationModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(IApplicationModule).GetTypeInfo().IsAssignableFrom(type);
        }

        internal static void CheckIsApplicationModuleType(Type moduleType)
        {
            if (!IsApplicationModule(moduleType))
            {
                throw new ArgumentException("Given type is not an Zeus module: " + moduleType.AssemblyQualifiedName);
            }
        }

        public virtual Task OnPostApplicationInitializationAsync([NotNull] IApplicationInitializationContext context)
        {
            OnPostApplicationInitialization(context);
            return Task.CompletedTask;
        }

        public virtual void OnPostApplicationInitialization([NotNull] IApplicationInitializationContext context)
        {

        }

        public virtual Task OnPreApplicationInitializationAsync([NotNull] IApplicationInitializationContext context)
        {
            OnPreApplicationInitialization(context);
            return Task.CompletedTask;
        }

        public virtual void OnPreApplicationInitialization([NotNull] IApplicationInitializationContext context)
        {

        }

        public virtual Task OnApplicationInitializationAsync([NotNull] IApplicationInitializationContext context)
        {
            OnPreApplicationInitialization(context);
            return Task.CompletedTask;
        }

        public virtual void OnApplicationInitialization([NotNull] IApplicationInitializationContext context)
        {

        }

        protected void Configure<TOptions>(Action<TOptions> configureOptions)
        where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure(configureOptions);
        }

        protected void Configure<TOptions>(string name, Action<TOptions> configureOptions)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure(name, configureOptions);
        }

        protected void Configure<TOptions>(IConfiguration configuration)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure<TOptions>(configuration);
        }

        protected void Configure<TOptions>(IConfiguration configuration, Action<BinderOptions> configureBinder)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure<TOptions>(configuration, configureBinder);
        }

        protected void Configure<TOptions>(string name, IConfiguration configuration)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure<TOptions>(name, configuration);
        }

        protected void PreConfigure<TOptions>(Action<TOptions> configureOptions)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.PreConfigure(configureOptions);
        }

        protected void PostConfigure<TOptions>(Action<TOptions> configureOptions)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.PostConfigure(configureOptions);
        }

        protected void PostConfigureAll<TOptions>(Action<TOptions> configureOptions)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.PostConfigureAll(configureOptions);
        }

        public virtual Task PreConfigureServicesAsync(IApplicationServiceConfiguration context)
        {
            PreConfigureServices(context);
            return Task.CompletedTask;
        }

        public virtual void PreConfigureServices(IApplicationServiceConfiguration context)
        {

        }

        public virtual Task OnApplicationShutdownAsync([NotNull] IApplicationShutdownContext context)
        {
            OnApplicationShutdown(context);
            return Task.CompletedTask;
        }

        public virtual void OnApplicationShutdown([NotNull] IApplicationShutdownContext context)
        {

        }

        public virtual Task PostConfigureServicesAsync(IApplicationServiceConfiguration context)
        {
            PostConfigureServices(context);
            return Task.CompletedTask;
        }

        public virtual void PostConfigureServices(IApplicationServiceConfiguration context)
        {

        }
    }

}
