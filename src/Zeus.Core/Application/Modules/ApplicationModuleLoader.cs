using Microsoft.Extensions.DependencyInjection;
using Zeus.Core.Application.Abstraction;
using Zeus.Core.Application.Exceptions;
using Zeus.Core.Application.Modules.Abstractions;
using Zeus.Core.Extensions.Collections;
using Zeus.Core.Extensions.DependencyInjection;

namespace Zeus.Core.Application.Modules
{
    public class ApplicationModuleLoader : IApplicationModuleLoader
    {
        public IApplicationModuleDescriptor[] LoadModules(
            IServiceCollection services,
            Type startupModuleType)
        {
            var modules = GetDescriptors(services, startupModuleType);

            modules = SortByDependency(modules, startupModuleType);

            return modules.ToArray();
        }

        private List<IApplicationModuleDescriptor> GetDescriptors(
            IServiceCollection services,
            Type startupModuleType)
        {
            var modules = new List<IApplicationModuleDescriptor>();

            FillModules(modules, services, startupModuleType);
            SetDependencies(modules);

            return modules.Cast<IApplicationModuleDescriptor>().ToList();
        }

        protected virtual void FillModules(
            List<IApplicationModuleDescriptor> modules,
            IServiceCollection services,
            Type startupModuleType)
        {
            var logger = services.GetInitLogger<ZeusApplicationBase>();

            //All modules starting from the startup module
            foreach (var moduleType in ModuleHelper.FindAllModuleTypes(startupModuleType, logger))
            {
                modules.Add(CreateModuleDescriptor(services, moduleType));
            }

            
        }

        protected virtual void SetDependencies(List<IApplicationModuleDescriptor> modules)
        {
            foreach (var module in modules)
            {
                SetDependencies(modules, module);
            }
        }

        protected virtual List<IApplicationModuleDescriptor> SortByDependency(List<IApplicationModuleDescriptor> modules, Type startupModuleType)
        {
            var sortedModules = modules.SortByDependencies(m => m.Dependencies);
            sortedModules.MoveItem(m => m.Type == startupModuleType, modules.Count - 1);
            return sortedModules;
        }

        protected virtual IApplicationModuleDescriptor CreateModuleDescriptor(IServiceCollection services, Type moduleType, bool isLoadedAsPlugIn = false)
        {
            return new ApplicationModuleDescriptor(moduleType, CreateAndRegisterModule(services, moduleType), isLoadedAsPlugIn);
        }

        protected virtual IApplicationModule CreateAndRegisterModule(IServiceCollection services, Type moduleType)
        {
            var module = (IApplicationModule)Activator.CreateInstance(moduleType)!;
            services.AddSingleton(moduleType, module);
            return module;
        }

        protected virtual void SetDependencies(List<IApplicationModuleDescriptor> modules, IApplicationModuleDescriptor module)
        {
            foreach (var dependedModuleType in ModuleHelper.FindDependedModuleTypes(module.Type))
            {
                var dependedModule = modules.FirstOrDefault(m => m.Type == dependedModuleType);
                if (dependedModule == null)
                {
                    throw new ZeusException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + module.Type.AssemblyQualifiedName);
                }

                module.AddDependency(dependedModule);
            }
        }
    }
}
