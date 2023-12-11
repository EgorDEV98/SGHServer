﻿using System.Net;

namespace SGHServer.Application.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message)
        : base(message, HttpStatusCode.NotFound)
    { }
}