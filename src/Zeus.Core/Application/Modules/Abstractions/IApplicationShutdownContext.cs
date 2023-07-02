namespace Zeus.Core.Application.Modules.Abstractions
{
    public interface IApplicationShutdownContext
    {
        IServiceProvider ServiceProvider { get; }
    }
}
