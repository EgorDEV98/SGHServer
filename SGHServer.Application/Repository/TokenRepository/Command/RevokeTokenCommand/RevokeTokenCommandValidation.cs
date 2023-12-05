using FluentValidation;

namespace SGHServer.Application.Repository.TokenRepository.Command.RevokeTokenCommand;

public class RevokeTokenCommandValidation : AbstractValidator<RevokeTokenCommand>
{
    public RevokeTokenCommandValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .WithErrorCode("400")
            .WithName(x => nameof(x.Email));
    }
}