using AuthAPI.Application.Requests.Account;
using AuthAPI.Application.Responses.Account;
using MediatR;

namespace AuthAPI.Application.Commands.Account.Login;

public record LoginCommand(
    LoginRequest Request) : IRequest<LoginResponse>;
