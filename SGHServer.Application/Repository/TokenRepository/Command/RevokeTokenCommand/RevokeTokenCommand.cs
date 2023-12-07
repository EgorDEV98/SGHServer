using MediatR;

namespace SGHServer.Application.Repository.TokenRepository.Command.RevokeTokenCommand;

public class RevokeTokenCommand : IRequest
{
    public string? Email { get; set; }
}