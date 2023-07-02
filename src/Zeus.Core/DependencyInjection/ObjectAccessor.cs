using Zeus.Core.DependencyInjection.Abstractions;

namespace Zeus.Core.DependencyInjection
{
    public class ObjectAccessor<T> : IObjectAccessor<T>
    {
        public T? Value { get; set; }

        public ObjectAccessor()
        {

        }

        public ObjectAccessor(T? obj)
        {
            Value = obj;
        }
    }
}
