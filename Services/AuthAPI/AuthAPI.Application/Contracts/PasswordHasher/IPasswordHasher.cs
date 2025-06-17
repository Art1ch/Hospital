namespace AuthAPI.Application.Contracts.PasswordHasher;

public interface IPasswordHasher
{
    bool VerifyPassword(string givenPassword, string hashPassword);
    string GeneratePassword(string givenPassword);
}
