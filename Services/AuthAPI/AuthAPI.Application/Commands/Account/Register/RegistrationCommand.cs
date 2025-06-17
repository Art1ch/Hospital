using AuthAPI.Application.Requests.Account;
using AuthAPI.Application.Responses.Account;
using MediatR;

namespace AuthAPI.Application.Commands.Account.Register;

public record RegistrationCommand(
    RegistrationRequest Request) : IRequest<RegistrationResponse>;
