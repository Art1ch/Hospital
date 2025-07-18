using AppointmentAPI.Application.Contracts.RemoteCaller;

namespace AppointmentAPI.Infrastructure.Services.RemoteCaller;

internal class RemoteCaller : IRemoteCaller
{
    public Task<string> GetDoctorsEmail(Guid DoctorId)
    {
        throw new NotImplementedException();
    }
}
