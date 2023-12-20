using SGHServer.Application.Response.VMs.UserInformation;
using SGHServer.Domain;

namespace SGHServer.Application.Mapping;

public static class UserMapping
{
    public static UserInformationVM ToVM(this User user)
        => new UserInformationVM()
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name
        };
}