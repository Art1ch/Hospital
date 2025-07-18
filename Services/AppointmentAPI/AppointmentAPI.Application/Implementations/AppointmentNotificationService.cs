using AppointmentAPI.Application.Contracts.Email;
using AppointmentAPI.Application.Contracts.RemoteCaller;
using AppointmentAPI.Application.Contracts.Repository.Appointment;
using AppointmentAPI.Application.Models;
using AppointmentAPI.Application.Contracts.AppointmentNotificationService;

namespace AppointmentAPI.Application.Implementations;

internal class AppointmentNotificationService(
    IEmailService emailService,
    IRemoteCaller remoteCaller,
    IAppointmentRepository repository
) : IAppointmentNotificationService
{
    private const int NotificationMinutesBefore = 10;

    public async Task NotifyDoctorsAboutAppointment(CancellationToken cancellationToken = default)
    {
        var doctorsIds = await repository.GetUpcomingAppointmentsDoctorsIds(NotificationMinutesBefore, cancellationToken);
        foreach (var id in doctorsIds)
        {
            var doctorEmail = await remoteCaller.GetDoctorsEmail(id);
            var message = new MessageModel()
            {
                Subject = "Appointment",
                HtmlBody = "AppointmentBody"
            };
            await emailService.SendMessage(doctorEmail, message);
        }
    }
}
