using System.Net;

namespace SGHServer.Application.Exceptions;

public class BaseException : Exception
{
    public HttpStatusCode StatusCode { get; private set; }
    
    public BaseException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}