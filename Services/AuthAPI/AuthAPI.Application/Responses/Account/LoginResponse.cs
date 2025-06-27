namespace AuthAPI.Application.Responses.Account;

public sealed record LoginResponse(
    bool IsSuccess,
    string? ReferenceToken,
    string? FailureMessage);
