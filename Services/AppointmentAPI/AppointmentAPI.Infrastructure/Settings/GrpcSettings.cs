namespace AppointmentAPI.Infrastructure.Settings;

public sealed class GrpcSettings
{
    public string AuthServiceAddress { get; set; }
    public string DoctorServiceAddress { get; set; }
}
