using System.Net;
using FluentValidation;

namespace SGHServer.API.Middleware;

public static class GlobalExceptionMiddlewareExtention
{
    public static IApplicationBuilder UseGlobalExtentionMiddleware(this IApplicationBuilder builder)
        => builder.UseMiddleware<GlobalExceptionMiddleware>();
}

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await ExceptionHandleAsync(context, exception);
        }
    }

    private async Task ExceptionHandleAsync(HttpContext httpContext, Exception exception)
    {
        int statusCode = (int)HttpStatusCode.BadRequest;
        string[] errorMessages = { "Internal Error" };
        switch (exception)
        {
            case ValidationException validationException:
                errorMessages = validationException.Errors
                    .Select(x => $"[{x.ErrorCode}] {x.ErrorMessage}")
                    .ToArray();
                break;
            
        }
        
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(new { errors = errorMessages });
    }
}