using FluentValidation;
using SGHServer.Application.Extentions;

namespace SGHServer.Application.Repository.DeviceRepository.Command.AddDeviceCommand;

public class AddDeviceCommandValidation : AbstractValidator<AddDeviceCommand>
{
    public AddDeviceCommandValidation()
    {
        RuleFor(x => x.DeviceUid.ToString())
            .NotNull().WithMessage("GUID устройства не может быть пустым").WithErrorCode("400")
            .NotEmpty().WithMessage("GUID устройства не может быть пустым").WithErrorCode("400")
            .IsGuid();
    }
}

