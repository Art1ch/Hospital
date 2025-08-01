namespace AppointmentAPI.Application.Contracts.RemoteCaller;

public interface IRemoteCaller
{
    Task<IEnumerable<string>> GetDoctorsEmailsAsync(IEnumerable<Guid> doctorsIds);
}
