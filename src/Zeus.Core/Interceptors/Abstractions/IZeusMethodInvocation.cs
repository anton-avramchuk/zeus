﻿using System.Reflection;

namespace Zeus.Core.Interceptors.Abstractions
{
    public interface IZeusMethodInvocation
    {
        object[] Arguments { get; }

        IReadOnlyDictionary<string, object> ArgumentsDictionary { get; }

        Type[] GenericArguments { get; }

        object TargetObject { get; }

        MethodInfo Method { get; }

        object ReturnValue { get; set; }

        Task ProceedAsync();
    }
}
