using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGHServer.API.RequestModel;
using SGHServer.Application.Repository.DeviceRepository.Command.AddDeviceCommand;
using SGHServer.Application.Repository.DeviceRepository.Command.RemoveDeviceCommand;

namespace SGHServer.API.Controllers;

[ApiController]
public class DeviceController : BaseController
{
    [HttpPost, Authorize]
    public async Task Add([FromBody] CreateDeviceModel createDeviceModel)
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
        int.TryParse(userId, out var intId);
        
        var command = new AddDeviceCommand()
        {
            Id = intId,
            DeviceUid = createDeviceModel.DeviceUid
        };
        await Mediator.Send(command);

        Ok();
    }

    [HttpDelete, Authorize]
    public async Task Remove([FromBody] RemoveDeviceModel removeDeviceModel)
    {
        var command = new RemoveDeviceCommand()
        {
            DeviceUid = removeDeviceModel.DeviceUid
        };
        await Mediator.Send(command);

        NoContent();
    }
}