using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Zeus.Core.Application.Modules.Abstractions;
using Zeus.Core.Extensions.Collections;

namespace Zeus.Core.Application.Modules
{
    //todo: test
    public class ApplicationModuleDescriptor : IApplicationModuleDescriptor
    {
        public Type Type { get; }

        public Assembly Assembly { get; }

        public IApplicationModule Instance { get; }

        public bool IsLoadedAsPlugIn { get; }

        public IReadOnlyList<IApplicationModuleDescriptor> Dependencies => _dependencies.ToImmutableList();
        private readonly List<IApplicationModuleDescriptor> _dependencies;

        public ApplicationModuleDescriptor(
            [NotNull] Type type,
            [NotNull] IApplicationModule instance,
            bool isLoadedAsPlugIn)
        {
            

            if (!type.GetTypeInfo().IsAssignableFrom(instance.GetType()))
            {
                throw new ArgumentException($"Given module instance ({instance.GetType().AssemblyQualifiedName}) is not an instance of given module type: {type.AssemblyQualifiedName}");
            }

            Type = type;
            Assembly = type.Assembly;
            Instance = instance;
            IsLoadedAsPlugIn = isLoadedAsPlugIn;

            _dependencies = new List<IApplicationModuleDescriptor>();
        }

        public void AddDependency(IApplicationModuleDescriptor descriptor)
        {
            _dependencies.AddIfNotContains(descriptor);
        }

        public override string ToString()
        {
            return $"[AbpModuleDescriptor {Type.FullName}]";
        }
    }
}
