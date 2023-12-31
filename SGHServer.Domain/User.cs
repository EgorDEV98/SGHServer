﻿namespace SGHServer.Domain;

public class User : BaseEntity
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Е майл пользователя
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Токен
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Дата окончания токена
    /// </summary>
    public DateTime RefreshTokenExpiryTime { get; set; }
    
    /// <summary>
    /// Устройства пользователя
    /// </summary>
    public ICollection<Device> Devices { get; set; }
}


