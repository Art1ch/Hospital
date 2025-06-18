using AuthAPI.Application.Contracts.TokenProvider;
using AuthAPI.Configuration.JwtSettings;
using AuthAPI.Core.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthAPI.Infrastructure.Implemenations.TokenProviderImplementation;

internal class TokenProvider : ITokenProvider
{
    private readonly int _randomNumberForRefreshToken = 32;
    private readonly int _randomNumberForReferenceToken = 16;
    private readonly JwtSettings _jwtSettings;
    private readonly SymmetricSecurityKey _securityKey;

    public TokenProvider(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
        _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
    }

    public string GenerateAccessToken(AccountEntity account)
    {
        var claims = new Claim[]
        {
            new Claim("token_type", "access"),
            new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, account.Email),
            new Claim(JwtRegisteredClaimNames.PhoneNumber, account.PhoneNumber)
        };

        var expiry = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryMinutes);
        var token = GenerateJwtToken(expiry, claims);

        return token;
    }

    public string GenerateIdToken(AccountEntity account)
    {
        var claims = new Claim[]
        {
            new Claim("token_type", "id"),
            new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
            new Claim("Role", nameof(account.Role)),
        };

        var expiry = DateTime.UtcNow.AddMinutes(_jwtSettings.IdTokenExpiryMinutes);
        var token = GenerateJwtToken(expiry, claims);

        return token;
    }

    public ReferenceTokenEntity GenerateReferenceToken(AccountEntity account)
    {
        var token = GenerateDefaultToken(new byte[_randomNumberForReferenceToken]);
        return new ReferenceTokenEntity()
        {
            Token = token,
            CreatedAt = DateTime.UtcNow,
            AccountId = account.Id,
            Account = account,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ReferenceTokenExpiryMinutes),
        };
    }

    public RefreshTokenEntity GenerateRefreshToken(AccountEntity account)
    {
        var token = GenerateDefaultToken(new byte[_randomNumberForRefreshToken]);
        return new RefreshTokenEntity()
        {
            Token = token,
            CreatedAt = DateTime.UtcNow,
            AccountId = account.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays),
        };
    }

    public bool IsTokenExpired(DateTime expiresAt)
    {
        var isExpired = DateTime.UtcNow > expiresAt;
        return isExpired;
    }

    private string GenerateDefaultToken(byte[] randomNumber)
    {
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        var token = Convert.ToBase64String(randomNumber);
        return token;
    }

    private string GenerateJwtToken(DateTime expiry, Claim[] claims)
    { 
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiry,
            SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha384Signature),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}