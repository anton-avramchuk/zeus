using System.Runtime.Serialization;

namespace Zeus.Core.Application.Exceptions
{
    public class ZeusInitializationException : ZeusException
    {
        public ZeusInitializationException()
        {

        }

        public ZeusInitializationException(string message)
            : base(message)
        {

        }

        public ZeusInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public ZeusInitializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
