using System.Net;

namespace SGHServer.Application.Exceptions;

public class UnauthorizedException : BaseException
{
    public UnauthorizedException(string message)
        : base(message, HttpStatusCode.Unauthorized)
    { }
}