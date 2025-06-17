using AuthAPI.Application.Requests.Token;
using AuthAPI.Application.Responses.Token;
using MediatR;

namespace AuthAPI.Application.Commands.Token.ExchangeToken;

public record ExchangeTokenCommand(
    ExchangeTokenRequest Request) : IRequest<ExchangeTokenResponse>;
