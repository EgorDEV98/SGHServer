using FluentValidation;

namespace SGHServer.Application.Repository.AccountRepository.Command.RegisterAccountCommand;

public class RegisterAccountCommandValidation : AbstractValidator<RegisterAccountCommand>
{
    public RegisterAccountCommandValidation()
    {
        RuleFor(x => x.Email)
            .NotNull().WithMessage("Адрес электронной почты обязателен!").WithErrorCode("400")
            .NotEmpty().WithMessage("Адрес электронной почты обязателен!").WithErrorCode("400")
            .EmailAddress().WithMessage("Не является действительным адресом электронной почты").WithErrorCode("400");

        RuleFor(x => x.Password)
            .NotNull().WithMessage("Пароль обязателен!").WithErrorCode("400")
            .NotEmpty().WithMessage("Пароль обязателен!").WithErrorCode("400");

        RuleFor(x => x.Password.Length)
            .GreaterThan(6).WithMessage("Пароль должен быть больше 6 символов!").WithErrorCode("400");
    }
}