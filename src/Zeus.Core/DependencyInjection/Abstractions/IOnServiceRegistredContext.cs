using Zeus.Core.Colllections;
using Zeus.Core.Interceptors.Abstractions;

namespace Zeus.Core.DependencyInjection.Abstractions
{
    public interface IOnServiceRegistredContext
    {
        ITypeList<IZeusInterceptor> Interceptors { get; }

        Type ImplementationType { get; }
    }
}
