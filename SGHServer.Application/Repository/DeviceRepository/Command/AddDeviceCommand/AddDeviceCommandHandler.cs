using MediatR;
using Microsoft.EntityFrameworkCore;
using SGHServer.Application.Exceptions;
using SGHServer.Application.Interfaces;
using SGHServer.Application.Mapping;
using SGHServer.Application.Response.VMs;
using SGHServer.Domain;

namespace SGHServer.Application.Repository.DeviceRepository.Command.AddDeviceCommand;

public class AddDeviceCommandHandler : IRequestHandler<AddDeviceCommand, DeviceVM>
{
    private readonly IDataStore _dataStore;

    public AddDeviceCommandHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task<DeviceVM> Handle(AddDeviceCommand request, CancellationToken cancellationToken)
    {
        var oldDevice = await _dataStore.Devices
            .FirstOrDefaultAsync(x => x.DeviceUid == request.DeviceUid, cancellationToken);

        if (oldDevice != null)
        {
            throw new HasBeenException("Данное устройство уже было добавлено ранее");
        }

        var user = await _dataStore.Users
            .Include(x => x.Devices)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        var userDevices = user.Devices.ToList();
        var newDevice = new Device()
        {
            DeviceName = "Умная теплица #" + (userDevices.Count + 1),
            DeviceUid = request.DeviceUid
        };
        user.Devices.Add(newDevice);

        await _dataStore.SaveChangesAsync(cancellationToken);

        return newDevice.Map();
    }
}