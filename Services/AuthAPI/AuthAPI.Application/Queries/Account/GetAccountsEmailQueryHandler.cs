using AuthAPI.Application.Contracts.Repository.Account;
using AuthAPI.Core.Entities;
using MediatR;

namespace AuthAPI.Application.Queries.Account;

internal sealed class GetAccountsEmailQueryHandler(
    IAccountRepository accountRepository
) : IRequestHandler<GetAccountEntitiesQuery, IQueryable<AccountEntity>>
{
    public Task<IQueryable<AccountEntity>> Handle(GetAccountEntitiesQuery request, CancellationToken cancellationToken)
    { 
        return Task.FromResult(
            accountRepository.GetEntitiesQuery());
    }
}
