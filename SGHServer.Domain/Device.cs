namespace SGHServer.Domain;

public class Device : BaseEntity
{
    /// <summary>
    /// Строковое название устройства
    /// </summary>
    public string DeviceName { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор устройства
    /// </summary>
    public Guid DeviceUid { get; set; }
    
    /// <summary>
    /// Датчики устройства
    /// </summary>
    public ICollection<Sensor> Sensors { get; set; }
    
    /// <summary>
    /// Ссылка на пользователя
    /// </summary>
    public User User { get; set; }
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public int UserId { get; set; }
}