using Microsoft.Extensions.Logging;

namespace Zeus.Core.Logging.Abstractions
{
    public interface IInitLogger<out T> : ILogger<T>
    {
        public List<ZeusInitLogEntry> Entries { get; }
    }
}
