using System.Reflection;
using Zeus.Core.Application.Modules.Abstractions;

namespace Zeus.Core.Application.Modules
{
    public abstract class ApplicationModule : IApplicationModule
    {
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
    }

}
