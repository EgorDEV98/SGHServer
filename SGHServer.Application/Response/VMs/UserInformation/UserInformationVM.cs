namespace SGHServer.Application.Response.VMs.UserInformation;

public class UserInformationVM
{
    public int Id { get; set; }
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Е майл пользователя
    /// </summary>
    public string Email { get; set; }
}