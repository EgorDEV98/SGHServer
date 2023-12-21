using SGHServer.Application.Response.VMs.SensorVM;
using SGHServer.Domain;

namespace SGHServer.Application.Mapping;

public static class SensorMapping
{
    public static SensorVM[] Map(this Sensor[] sensors)
        => sensors
            .Select(sensor =>
            {
                var lastValue = sensor.Values.MaxBy(sv => sv.LastTimeUpdate);

                return new SensorVM
                {
                    Name = sensor.Name,
                    PostFix = sensor.PostFix,
                    SensorUid = sensor.SensorUid,
                    Value = lastValue?.Value ?? 0.0,
                    LastUpdate = lastValue?.LastTimeUpdate ?? DateTime.MinValue
                };
            })
            .ToArray();
}