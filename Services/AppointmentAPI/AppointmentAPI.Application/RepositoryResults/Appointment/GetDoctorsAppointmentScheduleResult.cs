namespace AppointmentAPI.Application.RepositoryResults.Appointment;

public sealed record GetDoctorsAppointmentScheduleResult(
    List<GetDoctorsAppointmentScheduleItem> Items);

