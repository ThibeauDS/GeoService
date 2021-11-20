using System;
using System.Runtime.Serialization;

namespace DomeinLaag.Exceptions
{
    [Serializable]
    public class ContinentException : Exception
    {
        public ContinentException()
        {
        }

        public ContinentException(string message) : base(message)
        {
        }

        public ContinentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ContinentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
