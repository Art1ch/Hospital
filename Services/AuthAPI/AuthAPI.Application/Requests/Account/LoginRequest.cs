namespace AuthAPI.Application.Requests.Account;

public record LoginRequest(
    string Email,
    string Password);
