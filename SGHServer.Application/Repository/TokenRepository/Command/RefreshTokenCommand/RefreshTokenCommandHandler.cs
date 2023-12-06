using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SGHServer.Application.Interfaces;
using SGHServer.Application.Response;

namespace SGHServer.Application.Repository.TokenRepository.Command.RefreshTokenCommand;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse?>
{
    private readonly IDataStore _dataStore;
    private readonly IIdentityService _identityService;
    private readonly ILogger<RefreshTokenCommandHandler> _logger;

    public RefreshTokenCommandHandler(
        IDataStore dataStore, 
        IIdentityService identityService, 
        ILogger<RefreshTokenCommandHandler> logger)
    {
        _dataStore = dataStore;
        _identityService = identityService;
        _logger = logger;
    }
    
    public async Task<AuthResponse?> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var principal = _identityService.GetPrincipalFromExpiredToken(request.AccessToken);
            var email = principal.Identity.Name;

            var user = await _dataStore.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

            if (user is null || user.RefreshToken != request.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                _logger.LogWarning(
                    "user is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now");
                return null;
            }

            var newAccessToken = _identityService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _identityService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _dataStore.SaveChangesAsync(cancellationToken);

            return new AuthResponse()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
}