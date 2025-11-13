using AuthAPI.Core.Entities;

namespace AuthAPI.Application.Commands.Token.ExchangeToken;

public sealed record ExchangeTokenResult(
    bool IsSuccess,
    string? IdToken,
    string? AccessToken,
    RefreshTokenEntity? RefreshToken,
    string? FailureMessage
);