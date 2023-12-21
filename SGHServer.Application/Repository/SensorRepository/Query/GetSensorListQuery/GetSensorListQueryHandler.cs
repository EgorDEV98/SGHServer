using MediatR;
using Microsoft.EntityFrameworkCore;
using SGHServer.Application.Exceptions;
using SGHServer.Application.Interfaces;
using SGHServer.Application.Mapping;
using SGHServer.Application.Response.VMs.SensorVM;

namespace SGHServer.Application.Repository.SensorRepository.Query.GetSensorListQuery;

public class GetSensorListQueryHandler : IRequestHandler<GetSensorListQuery, SensorListVM>
{
    private readonly IDataStore _dataStore;

    public GetSensorListQueryHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task<SensorListVM> Handle(GetSensorListQuery request, CancellationToken cancellationToken)
    {
        var device = await _dataStore.Devices
            .Include(x => x.Sensors)
            .Where(x => x.DeviceUid == request.DeviceUid)
            .FirstOrDefaultAsync(cancellationToken);

        if (device == null)
        {
            throw new NotFoundException("Устройство не найдено");
        }

        var sensors = device.Sensors.ToArray();

        return new SensorListVM()
        {
            Sensors = sensors.Map()
        };
    }
}