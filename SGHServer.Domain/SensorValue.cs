namespace SGHServer.Domain;

public class SensorValue : BaseEntity
{
    /// <summary>
    /// Значение
    /// </summary>
    public double Value { get; set; }
    
    /// <summary>
    /// Время последнего обновления
    /// </summary>
    public DateTime LastTimeUpdate { get; set; }
}