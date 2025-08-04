namespace AppointmentAPI.Application.RepositoryResults.Appointment;

public sealed record GetAppointmentsHistoryResult(
    IEnumerable<GetAppointmentsHistoryItem> Items
);
