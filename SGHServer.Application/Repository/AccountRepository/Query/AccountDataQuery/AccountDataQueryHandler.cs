using MediatR;
using Microsoft.Extensions.Logging;
using SGHServer.Application.Interfaces;
using SGHServer.Application.Models;

namespace SGHServer.Application.Repository.AccountRepository.Query.AccountDataQuery;

public class AccountDataQueryHandler : IRequestHandler<AccountDataQuery, AccountDataDto>
{
    private readonly IDataStore _dataStore;
    private readonly ILogger<AccountDataQueryHandler> _logger;

    public AccountDataQueryHandler(IDataStore dataStore, ILogger<AccountDataQueryHandler> logger)
    {
        _dataStore = dataStore;
        _logger = logger;
    }
    
    public Task<AccountDataDto> Handle(AccountDataQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}