namespace AppointmentAPI.Application.Contracts.RemoteCaller;

public interface IRemoteCaller
{
    Task<List<string>> GetDoctorsEmailsAsync(List<Guid> doctorsIds);
}
