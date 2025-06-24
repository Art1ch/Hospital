namespace AuthAPI.Configuration.JwtSettings;

public class JwtSettings
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public bool ValidateIssuer { get; set; } 
    public bool ValidateAudience { get; set; }
    public int IdTokenExpiryMinutes { get; set; } 
    public int AccessTokenExpiryMinutes { get; set; } 
    public int RefreshTokenExpiryDays { get; set; }
    public int ReferenceTokenExpiryMinutes { get; set; }
}
