using MediatR;

namespace SGHServer.Application.Repository.DeviceRepository.Command.RemoveDeviceCommand;

public class RemoveDeviceCommand : IRequest
{
    /// <summary>
    /// GUID устройства
    /// </summary>
    public Guid DeviceUid { get; set; }
}