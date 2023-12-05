using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SGHServer.Application.Interfaces;

namespace SGHServer.Application.Repository.TokenRepository.Command.RevokeTokenCommand;

public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, bool>
{
    private readonly IDataStore _dataStore;
    private readonly ILogger<RevokeTokenCommandHandler> _logger;

    public RevokeTokenCommandHandler(IDataStore dataStore, ILogger<RevokeTokenCommandHandler> logger)
    {
        _dataStore = dataStore;
        _logger = logger;
    }
    
    public async Task<bool> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var username = await _dataStore.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken: cancellationToken);
            if (username == null)
            {
                _logger.LogError("Пользователь с именем {Email} не найден", request.Email);
                return false;
            }

            username.RefreshToken = null;
            await _dataStore.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }
}