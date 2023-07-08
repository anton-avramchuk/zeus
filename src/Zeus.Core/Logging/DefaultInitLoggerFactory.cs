using Zeus.Core.Extensions.Collections;
using Zeus.Core.Logging.Abstractions;

namespace Zeus.Core.Logging
{
    public class DefaultInitLoggerFactory : IInitLoggerFactory
    {
        private readonly Dictionary<Type, object> _cache = new Dictionary<Type, object>();

        public virtual IInitLogger<T> Create<T>()
        {
            return (IInitLogger<T>)_cache.GetOrAdd(typeof(T), () => new DefaultInitLogger<T>()); ;
        }
    }
}
