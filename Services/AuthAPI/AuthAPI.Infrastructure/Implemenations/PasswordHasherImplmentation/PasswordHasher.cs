using AuthAPI.Application.Contracts.PasswordHasher;
using BCrypt.Net;

namespace AuthAPI.Infrastructure.Implemenations.PasswordHasherImplmentation;

internal class PasswordHasher : IPasswordHasher
{
    private readonly int _workFactor = 12;
    private readonly HashType _hashType = HashType.SHA384;

    public string GeneratePassword(string givenPassword)
    {
        var password = BCrypt.Net.BCrypt.EnhancedHashPassword(givenPassword, _workFactor, _hashType);
        return password;
    }

    public bool VerifyPassword(string givenPassword, string hashPassword)
    {
        var isVerified = BCrypt.Net.BCrypt.EnhancedVerify(givenPassword, hashPassword, _hashType);
        return isVerified;
    }
}
