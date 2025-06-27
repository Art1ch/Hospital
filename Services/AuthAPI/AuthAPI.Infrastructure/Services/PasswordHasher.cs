using AuthAPI.Application.Contracts.PasswordHasher;
using BCrypt.Net;

namespace AuthAPI.Infrastructure.Services;

internal class PasswordHasher : IPasswordHasher
{
    private const int _workFactor = 12;
    private const HashType _hashType = HashType.SHA384;

    public string GeneratePasswordHash(string givenPassword)
    {
        var password = BCrypt.Net.BCrypt.EnhancedHashPassword(givenPassword, _workFactor, _hashType);
        return password;
    }

    public bool VerifyPassword(string givenPassword, string accountHashPassword)
    {
        var isVerified = BCrypt.Net.BCrypt.EnhancedVerify(givenPassword, accountHashPassword, _hashType);
        return isVerified;
    }
}
