namespace AuthAPI.Application.Responses.Account;

public sealed record RegistrationResponse(
    bool IsSuccess,
    string? ReferenceToken);
