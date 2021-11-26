using System;
using System.Runtime.Serialization;

namespace DataLaag.Exceptions
{
    [Serializable]
    public class StadRepositoryADOException : Exception
    {
        public StadRepositoryADOException()
        {
        }

        public StadRepositoryADOException(string message) : base(message)
        {
        }

        public StadRepositoryADOException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StadRepositoryADOException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
