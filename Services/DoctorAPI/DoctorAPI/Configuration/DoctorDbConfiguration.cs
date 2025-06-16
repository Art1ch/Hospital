namespace DoctorAPI.Configuration;

public class DoctorDbConfiguration
{
    public string ConnectionString { get; private set; }

    public DoctorDbConfiguration(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("DoctorDbString")!;
    }
}
