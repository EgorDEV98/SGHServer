using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SGHServer.Application.Exceptions;
using SGHServer.Application.Interfaces;

namespace SGHServer.Application.Repository.TokenRepository.Command.RevokeTokenCommand;

public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand>
{
    private readonly IDataStore _dataStore;
    private readonly ILogger<RevokeTokenCommandHandler> _logger;

    public RevokeTokenCommandHandler(IDataStore dataStore, ILogger<RevokeTokenCommandHandler> logger)
    {
        _dataStore = dataStore;
        _logger = logger;
    }
    
    public async Task Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var username = await _dataStore.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken: cancellationToken);
        if (username == null)
        {
            _logger.LogError("Пользователь с именем {Email} не найден", request.Email);
            throw new UnauthorizedException("Необходимо авторизоваться");
        }

        username.RefreshToken = null;
        await _dataStore.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Пользователь {Email} успешно вышел", request.Email);
    }
}