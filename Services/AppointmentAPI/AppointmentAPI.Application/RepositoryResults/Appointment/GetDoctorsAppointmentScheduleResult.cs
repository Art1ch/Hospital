namespace AppointmentAPI.Application.RepositoryResults.Appointment;

public sealed record GetDoctorsAppointmentScheduleResult(
    IEnumerable<GetDoctorsAppointmentScheduleItem> Items
);

