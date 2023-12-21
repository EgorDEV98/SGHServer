namespace SGHServer.Domain;

public class Sensor : BaseEntity
{
    /// <summary>
    /// GUID датчика
    /// </summary>
    public Guid SensorUid { get; set; }
    
    /// <summary>
    /// Имя датчика
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Значения с датчика
    /// </summary>
    public ICollection<SensorValue> Values { get; set; }
}