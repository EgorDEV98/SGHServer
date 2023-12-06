using MediatR;
using SGHServer.Application.Response;

namespace SGHServer.Application.Repository.TokenRepository.Command.RefreshTokenCommand;

public class RefreshTokenCommand : IRequest<AuthResponse?>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}