using System.Net;
using System.Runtime.Serialization;

namespace ClienteNet6.Server.Services.Exceptions
{
    /// <summary>
    /// indicate <see cref="HttpStatusCode.NoContent"/>
    /// </summary>
    public class NoContentException : Exception
    {
        public static HttpStatusCode StatusCode => HttpStatusCode.NoContent;

        public NoContentException()
        {
        }

        public NoContentException(string message) : base(message)
        {
        }

        public NoContentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoContentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
