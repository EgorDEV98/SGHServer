namespace SGHServer.Application.Response.VMs;

public class DeviceVM
{
    /// <summary>
    /// Строковое название устройства
    /// </summary>
    public string DeviceName { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор устройства
    /// </summary>
    public Guid DeviceUid { get; set; }
}