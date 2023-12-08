using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SGHServer.Application.Exceptions;
using SGHServer.Application.Interfaces;
using SGHServer.Application.Response;
using SGHServer.Application.Utils;
using SGHServer.Domain;

namespace SGHServer.Application.Repository.AccountRepository.Command.RegisterAccountCommand;

public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommand, AuthResponse>
{
    private readonly IDataStore _dataStore;
    private readonly IIdentityService _identityService;
    private readonly ILogger<RegisterAccountCommandHandler> _logger;

    public RegisterAccountCommandHandler(
        IDataStore dataStore, 
        IIdentityService identityService, 
        ILogger<RegisterAccountCommandHandler> logger)
    {
        _dataStore = dataStore;
        _identityService = identityService;
        _logger = logger;
    }
    
    
    public async Task<AuthResponse> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, request.Email),
            new Claim(ClaimTypes.Role, "User")
        };
        
        var accessToken = _identityService.GenerateAccessToken(claims);
        var refreshToken = _identityService.GenerateRefreshToken();
        
        var hashedPassword = Hashed.Encrypt(request.Password);

        var userAny = await _dataStore.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (userAny != null)
        {
            throw new BadRequestException("Пользователь с данным email уже зарегистрирован!");
        }

        var user = new User()
        {
            Email = request.Email,
            Password = hashedPassword,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
        };

        await _dataStore.Users.AddAsync(user, cancellationToken);
        await _dataStore.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Пользователь с Email {Email} успешно создан с ID {ID}", request.Email, user.Id);

        return new AuthResponse()
        {
            RefreshToken = refreshToken,
            AccessToken = accessToken
        };
    }
}