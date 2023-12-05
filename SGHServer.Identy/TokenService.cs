using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SGHServer.Application.Interfaces;

namespace SGHServer.Identy;

public class TokenService : IIdentityService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secretKeyString = _configuration["SGHSERVER_JWT:SECRET"];
        var issuer = _configuration["SGHSERVER_JWT:ISSUER"];
        var audience = _configuration["SGHSERVER_JWT:AUDIENCE"];
        var liftimeJwt = _configuration["SGHSERVER_JWT:EXPIRES"];
        Int32.TryParse(liftimeJwt, out var lifeTimeToken);
        
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyString));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(lifeTimeToken),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        return tokenString;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, GetTokenParameter(), out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
        return principal;
    }

    private TokenValidationParameters GetTokenParameter()
    {
        return new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SGHSERVER_JWT:SECRET"])),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    }
}