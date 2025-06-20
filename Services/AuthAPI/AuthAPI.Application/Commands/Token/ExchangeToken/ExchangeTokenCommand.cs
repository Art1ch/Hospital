using AuthAPI.Application.Abstractions;
using AuthAPI.Application.Requests.Token;
using AuthAPI.Application.Responses.Token;
using MediatR;

namespace AuthAPI.Application.Commands.Token.ExchangeToken;

public sealed record ExchangeTokenCommand(
    ExchangeTokenRequest Request) : BaseRequest<ExchangeTokenRequest, ExchangeTokenResponse>(Request);
