using System.Runtime.Serialization;

namespace Zeus.Core.Application.Exceptions
{
    public class ZeusException : Exception
    {
        public ZeusException()
        {

        }

        public ZeusException(string message)
            : base(message)
        {

        }

        public ZeusException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public ZeusException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}
