using AuthAPI.Application.Abstractions;
using AuthAPI.Application.Requests.Account;
using AuthAPI.Application.Responses.Account;

namespace AuthAPI.Application.Commands.Account.Register;

public sealed record RegistrationCommand(
    RegistrationRequest Request) : BaseRequest<RegistrationRequest, RegistrationResponse>(Request);
