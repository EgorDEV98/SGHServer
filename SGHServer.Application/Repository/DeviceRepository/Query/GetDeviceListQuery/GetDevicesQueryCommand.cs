using MediatR;
using SGHServer.Application.Response.VMs;

namespace SGHServer.Application.Repository.DeviceRepository.Query.GetDevicesQuery;

public class GetDevicesQueryCommand : IRequest<DeviceVmList>
{
    public int UserId { get; set; }
}