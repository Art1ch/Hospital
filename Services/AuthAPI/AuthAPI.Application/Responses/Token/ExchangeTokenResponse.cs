namespace AuthAPI.Application.Responses.Token;

public sealed record ExchangeTokenResponse(
    string IdToken,
    string AccessToken,
    string RefreshToken);
