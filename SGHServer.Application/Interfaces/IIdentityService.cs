using System.Security.Claims;

namespace SGHServer.Application.Interfaces;

public interface IIdentityService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}