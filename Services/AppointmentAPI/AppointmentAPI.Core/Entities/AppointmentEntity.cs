using AppointmentAPI.Core.Enums;

namespace AppointmentAPI.Core.Entities;

public class AppointmentEntity
{
    public Guid Id { get; set; }
    public Guid DoctorId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartAppointmentTime { get; set; }
    public TimeOnly EndAppointmentTime { get; set; }
    public AppointmentStatus Status { get; set; }
}
