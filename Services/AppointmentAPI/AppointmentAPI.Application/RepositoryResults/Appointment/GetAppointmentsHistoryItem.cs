namespace AppointmentAPI.Application.RepositoryResults.Appointment;

public sealed record GetAppointmentsHistoryItem(
    Guid DoctorId,
    DateOnly Date,
    TimeOnly StartAppointmentTime,
    TimeOnly EndAppointmentTime);
