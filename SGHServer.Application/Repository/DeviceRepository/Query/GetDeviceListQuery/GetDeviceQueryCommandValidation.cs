using FluentValidation;

namespace SGHServer.Application.Repository.DeviceRepository.Query.GetDevicesQuery;

public class GetDeviceQueryCommandValidation : AbstractValidator<GetDevicesQueryCommand>
{
    public GetDeviceQueryCommandValidation()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("Не удалось найти пользователя").WithErrorCode("401")
            .GreaterThan(0).WithMessage("Не удалось найти пользователя").WithErrorCode("401");
    }
}