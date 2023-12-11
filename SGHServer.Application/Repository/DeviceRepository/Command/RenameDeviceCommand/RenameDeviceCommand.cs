using MediatR;

namespace SGHServer.Application.Repository.DeviceRepository.Command.RenameDeviceCommand;

public class RenameDeviceCommand : IRequest
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