namespace SGHServer.API.RequestModel.Sensor;

public class AddSensorModel
{
    public Guid DeviceUid { get; set; }
    public Guid SensorUid { get; set; }
    public string Name { get; set; }
    public string Postfix { get; set; }
}