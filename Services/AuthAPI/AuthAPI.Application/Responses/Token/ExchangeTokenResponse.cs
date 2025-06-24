namespace AuthAPI.Application.Responses.Token;

public sealed record ExchangeTokenResponse(
    bool IsSuccess,
    string? IdToken,
    string? AccessToken,
    string? RefreshToken,
    string? FailureMessage);
