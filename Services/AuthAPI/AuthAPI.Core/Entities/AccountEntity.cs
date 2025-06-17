using AuthAPI.Core.Enums;
using System.Security.Claims;

namespace AuthAPI.Core.Entities;

public class AccountEntity
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string HashPassword { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsEmailVerified { get; set; }
    public Roles Role { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public RefreshTokenEntity RefreshToken { get; set; }
    public ReferenceTokenEntity ReferenceToken { get; set; }
}
