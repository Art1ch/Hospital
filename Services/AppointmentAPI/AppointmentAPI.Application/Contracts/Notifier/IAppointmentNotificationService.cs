namespace AppointmentAPI.Application.Contracts.AppointmentNotificationService;

public interface IAppointmentNotificationService
{
    Task NotifyDoctorsAboutAppointment(CancellationToken cancellationToken = default);
}
