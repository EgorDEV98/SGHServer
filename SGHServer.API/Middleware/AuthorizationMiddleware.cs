using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SGHServer.API.Middleware;

public static class AuthorizationMiddlewareExtention
{
    public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        => builder.UseMiddleware<AuthorizationMiddleware>();
}

public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bt_6qMpaJbt_6qMpaJ5ujoEDd5ujoEDd")),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out  _);
        }

        await _next(context);
    }
}