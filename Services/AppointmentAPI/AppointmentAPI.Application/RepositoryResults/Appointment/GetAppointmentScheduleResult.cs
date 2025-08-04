namespace AppointmentAPI.Application.RepositoryResults.Appointment;

public sealed record GetAppointmentScheduleResult(
    Guid DoctorId,
    DateOnly Date,
    TimeOnly StartAppointmentTime,
    TimeOnly EndAppointmentTime
);
