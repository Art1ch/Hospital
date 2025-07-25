using AuthAPI.Application.Queries.Account;
using Grpc.Core;
using MediatR;
using Shared.Protos.Auth;

namespace AuthAPI.Services;

public class AuthGrpcService(
    ISender sender
) : AuthService.AuthServiceBase
{
    public async override Task<GetAccountsEmailResponse> GetAccountsEmail(GetAccountsEmailRequest request, ServerCallContext context)
    {
        var accountId = Guid.Parse(request.AccountId);
        var query = new GetAccountsEmailQuery(accountId);
        var email = await sender.Send(query);
        var response = new GetAccountsEmailResponse()
        {
            Email = email
        };
        return response;
    }
}
