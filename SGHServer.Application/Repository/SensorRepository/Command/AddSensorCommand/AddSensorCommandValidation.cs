using FluentValidation;

namespace SGHServer.Application.Repository.SensorRepository.Command.AddSensorCommand;

public class AddSensorCommandValidation : AbstractValidator<AddSensorCommand>
{
    public AddSensorCommandValidation()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Имя датчика не может быть пустым").WithErrorCode("404")
            .NotEmpty().WithMessage("Имя датчика не может быть пустым").WithErrorCode("404");
        
        RuleFor(x => x.Postfix)
            .NotNull().WithMessage("Единица измерения не может быть пустой").WithErrorCode("404")
            .NotEmpty().WithMessage("Единица измерения не может быть пустой").WithErrorCode("404");
    }
}