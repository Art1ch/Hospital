namespace AppointmentAPI.Application.RepositoryResults.Appointment;

public sealed record GetAppointmentsHistoryResult(
    List<GetAppointmentsHistoryItem> Items);
