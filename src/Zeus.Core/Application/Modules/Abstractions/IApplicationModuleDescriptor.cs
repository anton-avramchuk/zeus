using System.Reflection;

namespace Zeus.Core.Application.Modules.Abstractions
{
    public interface IApplicationModuleDescriptor
    {
        Type Type { get; }

        Assembly Assembly { get; }

        IApplicationModule Instance { get; }

        bool IsLoadedAsPlugIn { get; }

        IReadOnlyList<IApplicationModuleDescriptor> Dependencies { get; }

        public void AddDependency(IApplicationModuleDescriptor descriptor);
    }
}
