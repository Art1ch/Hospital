namespace DoctorAPI.IntegrationTests.Settings;

internal class DbSettings
{
    public string ConnectionString { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;
    public string Image { get; set; } = "postgres:14-alpine";
}

