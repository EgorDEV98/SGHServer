using System.Net;
using FluentValidation;

namespace SGHServer.Application.Repository.TokenRepository.Command.RefreshTokenCommand;

public class RefreshTokenCommandValidation : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidation()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty()
            .NotNull()
            .WithErrorCode("400")
            .WithName(x => nameof(x.AccessToken));
        
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .NotNull()
            .WithErrorCode("400")
            .WithName(x => nameof(x.RefreshToken));
    }
}