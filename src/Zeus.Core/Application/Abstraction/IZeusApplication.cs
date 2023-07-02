namespace Zeus.Core.Application.Abstraction
{
    public interface IZeusApplication
    {
        /// <summary>
        /// Reference to the root service provider used by the application.
        /// This can not be used before initializing  the application.
        /// </summary>
        IServiceProvider ServiceProvider { get; }
    }
}
