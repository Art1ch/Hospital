namespace DoctorAPI.Configuration;

public class DoctorDbConfiguration
{
    public string DbConnectionString { get; private set; }

    public DoctorDbConfiguration(string dbConnectionString)
    {
        DbConnectionString = dbConnectionString;
    }
}
