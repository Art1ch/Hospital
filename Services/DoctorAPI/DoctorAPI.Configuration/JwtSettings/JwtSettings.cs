namespace DoctorAPI.Configuration.JwtSettings;

public class JwtSettings
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public bool ValidateIssuer { get; set; } 
    public bool ValidateAudience { get; set; }
}
