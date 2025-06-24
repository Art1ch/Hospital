namespace AuthAPI.Application.Contracts.PasswordHasher;

public interface IPasswordHasher
{
    bool VerifyPassword(string givenPassword, string accountHashPassword);
    string GeneratePasswordHash(string givenPassword);
}
