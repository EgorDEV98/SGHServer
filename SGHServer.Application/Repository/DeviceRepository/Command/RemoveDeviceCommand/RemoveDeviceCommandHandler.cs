using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SGHServer.Application.Exceptions;
using SGHServer.Application.Interfaces;

namespace SGHServer.Application.Repository.DeviceRepository.Command.RemoveDeviceCommand;

public class RemoveDeviceCommandHandler : IRequestHandler<RemoveDeviceCommand>
{
    private readonly IDataStore _dataStore;
    private readonly ILogger<RemoveDeviceCommandHandler> _logger;

    public RemoveDeviceCommandHandler(IDataStore dataStore, ILogger<RemoveDeviceCommandHandler> logger)
    {
        _dataStore = dataStore;
        _logger = logger;
    }
    
    public async Task Handle(RemoveDeviceCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _dataStore.Devices.FirstOrDefaultAsync(x => x.DeviceUid == request.DeviceUid, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException("Устройство не найдено!");
        }

        _dataStore.Devices.Remove(entity);
        await _dataStore.SaveChangesAsync(cancellationToken);
    }
}