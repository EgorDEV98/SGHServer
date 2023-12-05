using Microsoft.Extensions.DependencyInjection;
using SGHServer.Application.Interfaces;

namespace SGHServer.Identy;

public static class DependencyInjection
{
    public static IServiceCollection AddProtection(this IServiceCollection services)
    {
        services.AddTransient<IIdentityService, TokenService>();
        
        return services;
    }
}