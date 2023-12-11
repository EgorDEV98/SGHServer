using MediatR;
using Microsoft.EntityFrameworkCore;
using SGHServer.Application.Exceptions;
using SGHServer.Application.Interfaces;

namespace SGHServer.Application.Repository.DeviceRepository.Command.RenameDeviceCommand;

public class RenameDeviceCommandHandler : IRequestHandler<RenameDeviceCommand>
{
    private readonly IDataStore _dataStore;

    public RenameDeviceCommandHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task Handle(RenameDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await _dataStore.Devices.FirstOrDefaultAsync(x => x.DeviceUid == request.DeviceUid, cancellationToken);

        if (device == null)
        {
            throw new NotFoundException("Устройство с данным GUID не найдено");
        }

        device.DeviceName = request.NewName;

        await _dataStore.SaveChangesAsync(cancellationToken);
    }
}