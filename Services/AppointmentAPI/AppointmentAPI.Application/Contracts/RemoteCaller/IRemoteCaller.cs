namespace AppointmentAPI.Application.Contracts.RemoteCaller;

public interface IRemoteCaller
{
    Task<string> GetDoctorsEmailAsync(Guid doctorId);
}
