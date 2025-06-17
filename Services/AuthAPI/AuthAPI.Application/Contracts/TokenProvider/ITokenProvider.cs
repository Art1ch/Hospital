using AuthAPI.Core.Entities;
using System.Security.Claims;

namespace AuthAPI.Application.Contracts.TokenProvider;

internal interface ITokenProvider
{
    ClaimsIdentity? ValidateJwtToken(string jwtToken);
    bool IsTokenExpired(DateTime expiresAT);
    string GenerateAccessToken(AccountEntity account);
    string GenerateIdToken(AccountEntity account);
    RefreshTokenEntity GenerateRefreshToken(AccountEntity account);
    ReferenceTokenEntity GenerateReferenceToken(AccountEntity account);

}
