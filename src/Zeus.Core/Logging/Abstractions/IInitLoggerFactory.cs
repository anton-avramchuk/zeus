namespace Zeus.Core.Logging.Abstractions
{
    public interface IInitLoggerFactory
    {
        IInitLogger<T> Create<T>();
    }
}
