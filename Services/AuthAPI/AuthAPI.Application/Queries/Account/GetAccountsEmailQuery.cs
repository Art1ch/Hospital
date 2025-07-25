using AuthAPI.Application.Abstractions;

namespace AuthAPI.Application.Queries.Account;

public sealed record GetAccountsEmailQuery(
    Guid AccountId
) : BaseRequest<Guid, string>(AccountId);
