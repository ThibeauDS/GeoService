using System;
using System.Runtime.Serialization;

namespace DomeinLaag.Exceptions
{
    [Serializable]
    public class LandException : Exception
    {
        public LandException()
        {
        }

        public LandException(string message) : base(message)
        {
        }

        public LandException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
