using System;
using System.Runtime.Serialization;

namespace IsuExtra.Tools
{
    public class IsuExtraException : Exception
    {
        public IsuExtraException()
        {
        }

        public IsuExtraException(string message)
            : base(message)
        {
        }

        public IsuExtraException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected IsuExtraException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}