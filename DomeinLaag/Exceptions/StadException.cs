using System;
using System.Runtime.Serialization;

namespace DomeinLaag.Exceptions
{
    [Serializable]
    public class StadException : Exception
    {
        public StadException()
        {
        }

        public StadException(string message) : base(message)
        {
        }

        public StadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
