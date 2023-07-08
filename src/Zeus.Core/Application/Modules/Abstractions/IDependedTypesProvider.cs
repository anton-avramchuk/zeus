using System.Diagnostics.CodeAnalysis;

namespace Zeus.Core.Application.Modules.Abstractions
{
    public interface IDependedTypesProvider
    {
        Type[] GetDependedTypes();
    }
}
