using MediatR;
using SGHServer.Application.Response.VMs.UserInformation;

namespace SGHServer.Application.Repository.AccountRepository.Query.UserInformationQuery;

public class UserInformationCommand : IRequest<UserInformationVM>
{
    public int Id { get; set; }
}