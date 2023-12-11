using FluentValidation;
using SGHServer.Application.Extentions;

namespace SGHServer.Application.Repository.DeviceRepository.Command.RenameDeviceCommand;

public class RenameDeviceCommandValidation : AbstractValidator<RenameDeviceCommand>
{
    private const byte minLengh = 1;
    private const byte maxLengh = 70;
    
    public RenameDeviceCommandValidation()
    {
        RuleFor(x => x.NewName)
            .NotNull().WithMessage("Новое имя не может быть пустым!").WithErrorCode("400")
            .NotEmpty().WithMessage("Новое имя не может быть пустым!").WithErrorCode("400")
            .Length(minLengh, maxLengh).WithMessage($"Новое имя может содержать от {minLengh} до {maxLengh} символов!").WithErrorCode("400");

        RuleFor(x => x.DeviceUid)
            .NotNull().WithMessage("GUID устройства не может быть пустым!").WithErrorCode("400")
            .NotEmpty().WithMessage("GUID устройства не может быть пустым!").WithErrorCode("400")
            .IsGuid().WithMessage("Данный GUID не является реальным GUID").WithErrorCode("400");
    }
}