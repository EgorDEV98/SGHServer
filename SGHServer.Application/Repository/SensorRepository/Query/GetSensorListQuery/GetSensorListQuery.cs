using MediatR;
using SGHServer.Application.Response.VMs.SensorVM;

namespace SGHServer.Application.Repository.SensorRepository.Query.GetSensorListQuery;

public class GetSensorListQuery : IRequest<SensorListVM>
{
    public Guid DeviceUid { get; set; }
}