namespace AuthAPI.Configuration.JwtSettings;

public class JwtSettings
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public bool ValidateIssuer { get; set; } = true;
    public bool ValidateAudience { get; set; } = true;
    public int IdTokenExpiryMinutes { get; set; } = 60;
    public int AccessTokenExpiryMinutes { get; set; } = 15;
    public int RefreshTokenExpiryDays { get; set; } = 7;
    public int ReferenceTokenExpiryMinutes { get; set; } = 15;
}
