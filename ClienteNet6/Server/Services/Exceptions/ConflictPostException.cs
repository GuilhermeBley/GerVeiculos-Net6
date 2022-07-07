using System.Net;
using System.Runtime.Serialization;

namespace ClienteNet6.Server.Services.Exceptions
{
    /// <summary>
    /// indicate <see cref="HttpStatusCode.NoContent"/>
    /// </summary>
    public class ConflictPostException : Exception
    {
        public static HttpStatusCode StatusCode => HttpStatusCode.Conflict;


        public ConflictPostException()
        {
        }

        public ConflictPostException(string message) : base(message)
        {
        }

        public ConflictPostException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConflictPostException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
