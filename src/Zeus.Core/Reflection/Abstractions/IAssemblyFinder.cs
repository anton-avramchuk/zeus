﻿using System.Reflection;

namespace Zeus.Core.Reflection.Abstractions
{
    /// <summary>
    /// Used to get assemblies in the application.
    /// It may not return all assemblies, but those are related with modules.
    /// </summary>
    public interface IAssemblyFinder
    {
        IReadOnlyList<Assembly> Assemblies { get; }
    }
}
