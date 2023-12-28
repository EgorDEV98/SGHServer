using Microsoft.AspNetCore.Mvc;
using SGHServer.API.RequestModel.Sensor;
using SGHServer.Application.Repository.SensorRepository.Command.AddSensorCommand;
using SGHServer.Application.Repository.SensorRepository.Query.GetSensorListQuery;
using SGHServer.Application.Response.VMs.SensorVM;

namespace SGHServer.API.Controllers;

[ApiController]
public class SensorController : BaseController
{
    [HttpPost]
    public async Task Add([FromBody] AddSensorModel sensorModel)
    {
        var command = new AddSensorCommand()
        {
            DeviceUid = sensorModel.DeviceUid,
            SensorUid = sensorModel.SensorUid,
            Name = sensorModel.Name,
            Postfix = sensorModel.Postfix
        };

        await Mediator.Send(command);
    }

    [HttpGet("{deviceUid}")]
    public async Task<SensorListVM> GetSensorList(Guid deviceUid)
    {
        var command = new GetSensorListQuery()
        {
            DeviceUid = deviceUid
        };

        var sensorList = await Mediator.Send(command);

        return sensorList;
    }
}