using System.Net;

namespace SGHServer.Application.Exceptions;

public class HasBeenException : BaseException
{
    public HasBeenException(string message)
        : base(message, HttpStatusCode.Conflict)
    { }
}