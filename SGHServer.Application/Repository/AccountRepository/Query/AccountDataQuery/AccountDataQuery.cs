using MediatR;
using SGHServer.Application.Models;

namespace SGHServer.Application.Repository.AccountRepository.Query.AccountDataQuery;

public class AccountDataQuery : IRequest<AccountDataDto> { }