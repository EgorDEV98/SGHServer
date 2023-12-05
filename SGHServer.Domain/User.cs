namespace SGHServer.Domain;

public class User : BaseEntity
{
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
}