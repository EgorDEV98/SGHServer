using System.Net;

namespace SGHServer.Application.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException(string message)
        : base(message, HttpStatusCode.BadRequest)
    {
        
    }
}