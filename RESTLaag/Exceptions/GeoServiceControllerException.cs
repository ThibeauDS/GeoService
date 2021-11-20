using System;
using System.Runtime.Serialization;

namespace RESTLaag.Exceptions
{
    public class GeoServiceControllerException : Exception
    {
        public GeoServiceControllerException()
        {
        }

        public GeoServiceControllerException(string message) : base(message)
        {
        }

        public GeoServiceControllerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GeoServiceControllerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
