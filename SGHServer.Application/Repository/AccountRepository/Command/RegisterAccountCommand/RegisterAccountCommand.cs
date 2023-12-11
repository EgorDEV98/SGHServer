using MediatR;
using SGHServer.Application.Response;

namespace SGHServer.Application.Repository.AccountRepository.Command.RegisterAccountCommand;

public class RegisterAccountCommand : IRequest<AuthResponse>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}