﻿namespace SGHServer.Domain;

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
}