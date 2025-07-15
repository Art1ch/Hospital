namespace AppointmentAPI.Application.RepositoryResults.Appointment;

public record GetDoctorsAppointmentScheduleItem(
    DateOnly Date,
    TimeOnly StartAppointmentTime,
    TimeOnly EndAppointmentTime);
