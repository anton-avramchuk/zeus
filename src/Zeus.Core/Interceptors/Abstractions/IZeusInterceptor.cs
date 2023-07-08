namespace Zeus.Core.Interceptors.Abstractions
{
    public interface IZeusInterceptor
    {
        Task InterceptAsync(IZeusMethodInvocation invocation);
    }
}
