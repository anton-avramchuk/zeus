using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Zeus.Core.Application.Abstraction;
using Zeus.Core.Application.Modules.Abstractions;

namespace Zeus.Core.Application.Modules
{
    public abstract class ApplicationModule : IApplicationModule, IOnApplicationInitialization, IOnPreApplicationInitialization, IOnPostApplicationInitialization
    {
        public IServiceProvider ServiceProvider => throw new NotImplementedException();

        public void ConfigureServices(IApplicationServiceConfiguration config)
        {
            throw new NotImplementedException();
        }

        public Task ConfigureServicesAsync(IApplicationServiceConfiguration config)
        {
            throw new NotImplementedException();
        }

        public static bool IsAapplicationModule(Type type)
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
            if (!IsAapplicationModule(moduleType))
            {
                throw new ArgumentException("Given type is not an Zeus module: " + moduleType.AssemblyQualifiedName);
            }
        }

        public Task OnPostApplicationInitializationAsync([NotNull] IApplicationInitializationContext context)
        {
            throw new NotImplementedException();
        }

        public void OnPostApplicationInitialization([NotNull] IApplicationInitializationContext context)
        {
            throw new NotImplementedException();
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
