using MediatR;
using SGHServer.Application.Response.VMs;

namespace SGHServer.Application.Repository.DeviceRepository.Command.RenameDeviceCommand;

public class RenameDeviceCommand : IRequest<DeviceVM>
{
    /// <summary>
    /// Новое имя устройства
    /// </summary>
    public string NewName { get; set; }
    
    /// <summary>
    /// GUID устройства
    /// </summary>
    public Guid DeviceUid { get; set; }
}