using MediatR;
using SGHServer.Application.Response.VMs;

namespace SGHServer.Application.Repository.DeviceRepository.Query.GetDeviceQuery;

public class GetDeviceQueryCommand : IRequest<DeviceVM>
{
    public Guid DeviceUid { get; set; }
}