using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGHServer.API.RequestModel;
using SGHServer.Application.Repository.DeviceRepository.Command.AddDeviceCommand;
using SGHServer.Application.Repository.DeviceRepository.Command.RemoveDeviceCommand;
using SGHServer.Application.Repository.DeviceRepository.Command.RenameDeviceCommand;
using SGHServer.Application.Repository.DeviceRepository.Query.GetDevicesQuery;
using SGHServer.Application.Response.VMs;

namespace SGHServer.API.Controllers;

[ApiController]
public class DeviceController : BaseController
{
    [HttpPost, Authorize]
    public async Task<DeviceVM> Add([FromBody] CreateDeviceModel createDeviceModel)
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
        int.TryParse(userId, out var intId);
        
        var command = new AddDeviceCommand()
        {
            Id = intId,
            DeviceUid = createDeviceModel.DeviceUid
        };
        var device = await Mediator.Send(command);

        return device;
    }

    [HttpPatch, Authorize]
    public async Task<IActionResult> Rename([FromBody] RenameDeviceModel renameDeviceModel)
    {
        var command = new RenameDeviceCommand()
        {
            DeviceUid = renameDeviceModel.DeviceUid,
            NewName = renameDeviceModel.NewName
        };
        await Mediator.Send(command);

        return Ok();
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

    [HttpGet, Authorize]
    public async Task<DeviceVmList> GetAsync()
    {
        var usersid = User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
        int.TryParse(usersid, out var userId);
        
        var command = new GetDevicesQueryCommand()
        {
            UserId = userId
        };
        var result = await Mediator.Send(command);

        return result;
    }
}