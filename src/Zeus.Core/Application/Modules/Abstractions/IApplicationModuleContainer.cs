using System.Diagnostics.CodeAnalysis;

namespace Zeus.Core.Application.Modules.Abstractions
{
    public interface IApplicationModuleContainer
    {
        [NotNull]
        IReadOnlyList<IApplicationModuleDescriptor> Modules { get; }
    }
}
