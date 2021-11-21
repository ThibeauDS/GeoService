using System;
using System.Runtime.Serialization;

namespace DomeinLaag.Exceptions
{
    [Serializable]
    public class LandServiceException : Exception
    {
        public LandServiceException()
        {
        }

        public LandServiceException(string message) : base(message)
        {
        }

        public LandServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LandServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
