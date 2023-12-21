using MediatR;

namespace SGHServer.Application.Repository.SensorRepository.Command.AddSensorCommand;

public class AddSensorCommand : IRequest
{
    public Guid DeviceUid { get; set; }
    public Guid SensorUid { get; set; }
    public string Name { get; set; }
    public string Postfix { get; set; }
}