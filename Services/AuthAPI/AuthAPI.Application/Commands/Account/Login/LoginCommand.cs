using AuthAPI.Application.Abstractions;
using AuthAPI.Application.Requests.Account;
using AuthAPI.Application.Responses.Account;

namespace AuthAPI.Application.Commands.Account.Login;

public sealed record LoginCommand(
    LoginRequest Request) : BaseRequest<LoginRequest, LoginResponse>(Request);
