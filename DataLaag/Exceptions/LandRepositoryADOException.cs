using System;
using System.Runtime.Serialization;

namespace DataLaag.Exceptions
{
    [Serializable]
    public class LandRepositoryADOException : Exception
    {
        public LandRepositoryADOException()
        {
        }

        public LandRepositoryADOException(string message) : base(message)
        {
        }

        public LandRepositoryADOException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LandRepositoryADOException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
