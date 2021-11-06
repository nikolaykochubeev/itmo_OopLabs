using System;
using System.Runtime.Serialization;

namespace Backups.Tools
{
    public class BackupsException : Exception
    {
        public BackupsException()
        {
        }

        public BackupsException(string message)
            : base(message)
        {
        }

        public BackupsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected BackupsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}