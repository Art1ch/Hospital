namespace AppointmentAPI.Application.Contracts.RemoteCaller;

public interface IRemoteCaller
{
    Task<string> GetDoctorsEmail(Guid DoctorId);
}
