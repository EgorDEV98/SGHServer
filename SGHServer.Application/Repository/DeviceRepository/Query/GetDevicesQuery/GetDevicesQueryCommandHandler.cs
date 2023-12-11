using MediatR;
using Microsoft.EntityFrameworkCore;
using SGHServer.Application.Interfaces;
using SGHServer.Application.Mapping;
using SGHServer.Application.Response.VMs;

namespace SGHServer.Application.Repository.DeviceRepository.Query.GetDevicesQuery;

public class GetDevicesQueryCommandHandler : IRequestHandler<GetDevicesQueryCommand, DeviceVmList>
{
    private readonly IDataStore _dataStore;

    public GetDevicesQueryCommandHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task<DeviceVmList> Handle(GetDevicesQueryCommand request, CancellationToken cancellationToken)
    {
        var devices = await _dataStore.Devices
            .Where(x => x.UserId == request.UserId)
            .ToArrayAsync(cancellationToken);

        return new DeviceVmList()
        {
            Devices = devices.ToVm()
        };
    }
}