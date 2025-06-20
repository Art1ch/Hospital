using AuthAPI.Core.Entities;

namespace AuthAPI.Application.Contracts.TokenProvider;

public interface ITokenProvider
{
    bool IsTokenExpired(DateTime expiresAt);
    string GenerateAccessToken(AccountEntity account);
    string GenerateIdToken(AccountEntity account);
    RefreshTokenEntity GenerateRefreshToken(AccountEntity account);
    ReferenceTokenEntity GenerateReferenceToken(AccountEntity account);
}
