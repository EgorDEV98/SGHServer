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
    /// Время последнего обновления
    /// </summary>
    public DateTime LastTimeUpdate { get; set; }
}