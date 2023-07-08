using Microsoft.Extensions.Logging;
using System.Reflection;
using Zeus.Core.Application.Modules.Abstractions;
using Zeus.Core.Extensions.Collections;

namespace Zeus.Core.Application.Modules
{
    internal static class ModuleHelper
    {
        public static List<Type> FindAllModuleTypes(Type startupModuleType, ILogger logger)
        {
            var moduleTypes = new List<Type>();
            logger.Log(LogLevel.Information, "Loaded Application modules:");
            AddModuleAndDependenciesRecursively(moduleTypes, startupModuleType, logger);
            return moduleTypes;
        }

        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            ApplicationModule.CheckIsApplicationModuleType(moduleType);

            var dependencies = new List<Type>();

            var dependencyDescriptors = moduleType
                .GetCustomAttributes()
                .OfType<IDependedTypesProvider>();

            foreach (var descriptor in dependencyDescriptors)
            {
                foreach (var dependedModuleType in descriptor.GetDependedTypes())
                {
                    dependencies.AddIfNotContains(dependedModuleType);
                }
            }

            return dependencies;
        }

        private static void AddModuleAndDependenciesRecursively(
            List<Type> moduleTypes,
            Type moduleType,
            ILogger logger,
            int depth = 0)
        {
            ApplicationModule.CheckIsApplicationModuleType(moduleType);

            if (moduleTypes.Contains(moduleType))
            {
                return;
            }

            moduleTypes.Add(moduleType);
            logger.Log(LogLevel.Information, $"{new string(' ', depth * 2)}- {moduleType.FullName}");

            foreach (var dependedModuleType in FindDependedModuleTypes(moduleType))
            {
                AddModuleAndDependenciesRecursively(moduleTypes, dependedModuleType, logger, depth + 1);
            }
        }
    }
}
