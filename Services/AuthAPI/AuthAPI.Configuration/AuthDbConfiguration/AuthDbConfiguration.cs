namespace AuthAPI.Configuration.DbSettings;

public class AuthDbConfiguration
{
    public string DbConnectionString { get; set; }

    public AuthDbConfiguration(string dbConnectionString)
    {
        DbConnectionString = dbConnectionString;
    }
}
