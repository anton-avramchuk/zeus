using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Zeus.Core.Application.Abstraction;
using Zeus.Core.Application.Modules.Abstractions;

namespace Zeus.Core.Application.Modules
{
    public abstract class ApplicationModule : IApplicationModule, IOnApplicationInitialization, IOnPreApplicationInitialization, IOnPostApplicationInitialization
    {

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

        public virtual  Task OnPostApplicationInitializationAsync([NotNull] IApplicationInitializationContext context)
        {
            OnPostApplicationInitialization(context);
            return Task.CompletedTask;
        }

        public virtual void OnPostApplicationInitialization([NotNull] IApplicationInitializationContext context)
        {
            
        }

        public Task OnPreApplicationInitializationAsync([NotNull] IApplicationInitializationContext context)
        {
            throw new NotImplementedException();
        }

        public void OnPreApplicationInitialization([NotNull] IApplicationInitializationContext context)
        {
            throw new NotImplementedException();
        }

        public Task OnApplicationInitializationAsync([NotNull] IApplicationInitializationContext context)
        {
            throw new NotImplementedException();
        }

        public void OnApplicationInitialization([NotNull] IApplicationInitializationContext context)
        {
            throw new NotImplementedException();
        }
    }

}
