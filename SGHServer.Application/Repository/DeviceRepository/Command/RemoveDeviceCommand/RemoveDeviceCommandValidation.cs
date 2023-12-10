using FluentValidation;
using SGHServer.Application.Extentions;

namespace SGHServer.Application.Repository.DeviceRepository.Command.RemoveDeviceCommand;

public class RemoveDeviceCommandValidation : AbstractValidator<RemoveDeviceCommand>
{
    public RemoveDeviceCommandValidation()
    {
        RuleFor(x => x.DeviceUid)
            .NotEmpty()
            .NotNull()
            .IsGuid();
    }
}