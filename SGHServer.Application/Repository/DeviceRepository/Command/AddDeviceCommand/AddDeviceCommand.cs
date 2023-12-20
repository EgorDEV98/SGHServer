using MediatR;
using SGHServer.Application.Response.VMs;

namespace SGHServer.Application.Repository.DeviceRepository.Command.AddDeviceCommand;

public class AddDeviceCommand : IRequest<DeviceVM>
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// GUID устройства
    /// </summary>
    public Guid DeviceUid { get; set; }
}