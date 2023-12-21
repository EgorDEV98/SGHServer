namespace SGHServer.Application.Response.VMs.SensorVM;

public class SensorVM
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
    /// Единицы измерения
    /// </summary>
    public string PostFix { get; set; }
    
    /// <summary>
    /// Последний показатель
    /// </summary>
    public double Value { get; set; }
    
    /// <summary>
    /// Дата последних измерений
    /// </summary>
    public DateTime LastUpdate { get; set; }
}