using AuthAPI.Application.Contracts.Repository.Account;
using MediatR;

namespace AuthAPI.Application.Queries.Account;

internal sealed class GetAccountsEmailQueryHandler(
    IAccountRepository accountRepository
) : IRequestHandler<GetAccountsEmailQuery, string>
{
    public async Task<string> Handle(GetAccountsEmailQuery request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetAsync(request.AccountId);
        return account.Email;
    }
}
