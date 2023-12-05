using MediatR;

namespace SGHServer.Application.Repository.TokenRepository.Command.RevokeTokenCommand;

public class RevokeTokenCommand : IRequest<bool>
{
    public string? Email { get; set; }
}