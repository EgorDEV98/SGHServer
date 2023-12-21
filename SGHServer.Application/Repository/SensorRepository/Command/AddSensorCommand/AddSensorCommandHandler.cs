using MediatR;
using Microsoft.EntityFrameworkCore;
using SGHServer.Application.Exceptions;
using SGHServer.Application.Interfaces;
using SGHServer.Domain;

namespace SGHServer.Application.Repository.SensorRepository.Command.AddSensorCommand;

public class AddSensorCommandHandler : IRequestHandler<AddSensorCommand>
{
    private readonly IDataStore _dataStore;

    public AddSensorCommandHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task Handle(AddSensorCommand request, CancellationToken cancellationToken)
    {
        var device = await _dataStore.Devices
            .FirstOrDefaultAsync(x => x.DeviceUid == request.DeviceUid, cancellationToken);

        if (device == null)
        {
            throw new NotFoundException("Данное устройство не найдено!");
        }

        var sensor = new Sensor()
        {
            Name = request.Name,
            PostFix = request.Postfix,
            SensorUid = request.SensorUid
        };
        device.Sensors.Add(sensor);

        await _dataStore.SaveChangesAsync(cancellationToken);
    }
}