using SGHServer.Application.Response.VMs;
using SGHServer.Domain;

namespace SGHServer.Application.Mapping;

public static class DeviceMapping
{
    public static DeviceVM[] ToVm(this Device[] devices) 
        => devices.Select(x => new DeviceVM()
            {
                DeviceName = x.DeviceName,
                DeviceUid = x.DeviceUid
            }).ToArray();
}