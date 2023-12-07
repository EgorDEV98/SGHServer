using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SGHServer.Application.Exceptions;
using SGHServer.Application.Interfaces;
using SGHServer.Application.Response;
using SGHServer.Application.Utils;

namespace SGHServer.Application.Repository.AccountRepository.Command.LoginAccountCommand;

public class LoginAccountCommandHandler : IRequestHandler<LoginAccountCommand, AuthResponse>
{
    private readonly IDataStore _dataStore;
    private readonly IIdentityService _identityService;
    private readonly ILogger<LoginAccountCommandHandler> _logger;

    public LoginAccountCommandHandler(IDataStore dataStore, IIdentityService identityService, ILogger<LoginAccountCommandHandler> logger)
    {
        _dataStore = dataStore;
        _identityService = identityService;
        _logger = logger;
    }

    public async Task<AuthResponse> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = Hashed.Encrypt(request.Password);
        var user = await _dataStore.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email && x.Password == hashedPassword, cancellationToken);

        if (user == null)
        {
            throw new BadRequestException("Пользователь с данным именем не найден");
        }
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, request.Email),
            new Claim(ClaimTypes.Role, "User"),
        };
        
        var accessToken = _identityService.GenerateAccessToken(claims);
        var refreshToken = _identityService.GenerateRefreshToken();
        
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        
        await _dataStore.SaveChangesAsync(cancellationToken);

        return new AuthResponse()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}