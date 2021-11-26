using System;
using System.Runtime.Serialization;

namespace DomeinLaag.Exceptions
{
    [Serializable]
    public class StadServiceException : Exception
    {
        public StadServiceException()
        {
        }

        public StadServiceException(string message) : base(message)
        {
        }

        public StadServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StadServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
