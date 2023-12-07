using MediatR;
using SGHServer.Application.Response;

namespace SGHServer.Application.Repository.AccountRepository.Command.LoginAccountCommand;

public class LoginAccountCommand : IRequest<AuthResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}