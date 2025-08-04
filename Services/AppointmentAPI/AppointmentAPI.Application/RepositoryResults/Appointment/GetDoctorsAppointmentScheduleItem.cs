namespace AppointmentAPI.Application.RepositoryResults.Appointment;

public sealed record GetDoctorsAppointmentScheduleItem(
    DateOnly Date,
    TimeOnly StartAppointmentTime,
    TimeOnly EndAppointmentTime
);
