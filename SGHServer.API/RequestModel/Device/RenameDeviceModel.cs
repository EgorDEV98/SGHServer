namespace SGHServer.API.RequestModel;

public class RenameDeviceModel
{
    public Guid DeviceUid { get; set; }
    public string NewName { get; set; }
}