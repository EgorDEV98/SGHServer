using MediatR;
using Microsoft.EntityFrameworkCore;
using SGHServer.Application.Exceptions;
using SGHServer.Application.Interfaces;
using SGHServer.Application.Mapping;
using SGHServer.Application.Response.VMs;

namespace SGHServer.Application.Repository.DeviceRepository.Query.GetDeviceQuery;

public class GetDeviceQueryCommandHandler : IRequestHandler<GetDeviceQueryCommand, DeviceVM>
{
    private readonly IDataStore _dataStore;

    public GetDeviceQueryCommandHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task<DeviceVM> Handle(GetDeviceQueryCommand request, CancellationToken cancellationToken)
    {
        var device = await _dataStore.Devices
            .FirstOrDefaultAsync(x => x.DeviceUid == request.DeviceUid, cancellationToken);

        if (device == null)
        {
            throw new NotFoundException("Устройство не найдено!");
        }

        return device.Map();
    }
}