using MediatR;
using Microsoft.EntityFrameworkCore;
using SGHServer.Application.Exceptions;
using SGHServer.Application.Interfaces;
using SGHServer.Application.Mapping;
using SGHServer.Application.Response.VMs.UserInformation;

namespace SGHServer.Application.Repository.AccountRepository.Query.UserInformationQuery;

public class UserInformationQueryHandler : IRequestHandler<UserInformationCommand, UserInformationVM>
{
    private readonly IDataStore _dataStore;

    public UserInformationQueryHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task<UserInformationVM> Handle(UserInformationCommand request, CancellationToken cancellationToken)
    {
        var user = await _dataStore.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException("Данный пользователь не найден");
        }

        return user.ToVM();
    }
}