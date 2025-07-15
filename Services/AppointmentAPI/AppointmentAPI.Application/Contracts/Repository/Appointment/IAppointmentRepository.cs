using AppointmentAPI.Application.RepositoryResults.Appointment;
using AppointmentAPI.Core.Entities;
using AppointmentAPI.Core.Enums;

namespace AppointmentAPI.Application.Contracts.Repository.Appointment;

public interface IAppointmentRepository : IRepository<AppointmentEntity, Guid>
{
    Task<GetDoctorsAppointmentScheduleResult> GetDoctorsAppointmentScheduleAsync(Guid id, CancellationToken cancellationToken = default);
    Task ChangeAppointmentStatusAsync(Guid id, AppointmentStatus status, CancellationToken cancellationToken = default);
}
