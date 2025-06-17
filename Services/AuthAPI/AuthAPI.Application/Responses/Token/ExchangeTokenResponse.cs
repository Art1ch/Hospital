namespace AuthAPI.Application.Responses.Token;

public record ExchangeTokenResponse(
    string IdToken,
    string AccessToken,
    string RefreshToken);
