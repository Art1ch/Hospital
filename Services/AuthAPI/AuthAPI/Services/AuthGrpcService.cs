using AuthAPI.Application.Queries.Account;
using Grpc.Core;
using MediatR;
using Shared.Protos.Auth;

namespace AuthAPI.Services;

public class AuthGrpcService(
    ISender sender
) : AuthService.AuthServiceBase
{
    public async override Task<GetAccountsEmailsResponse> GetAccountsEmails(GetAccountsEmailsRequest request, ServerCallContext context)
    {
        var accountIds = request.AccountsIds.Select(Guid.Parse);
        var accountsQuery = await sender.Send(new GetAccountEntitiesQuery());
        var accountsEmail = accountsQuery
            .Where(a => accountIds.Contains(a.Id))
            .Select(a => a.Email);
        var response = new GetAccountsEmailsResponse();
        response.Emails.AddRange(accountsEmail);
        return response;
    }
}
