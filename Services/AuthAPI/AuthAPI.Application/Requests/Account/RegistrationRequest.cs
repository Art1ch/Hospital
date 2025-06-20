namespace AuthAPI.Application.Requests.Account;

public record RegistrationRequest(
    string Email,
    string? Password,
    string PhoneNumber);
