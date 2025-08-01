using AppointmentAPI.Application.Contracts.Email;
using AppointmentAPI.Application.Contracts.RemoteCaller;
using AppointmentAPI.Application.Contracts.Repository.Appointment;
using AppointmentAPI.Application.Models;
using AppointmentAPI.Application.Contracts.AppointmentNotificationService;

namespace AppointmentAPI.Application.Implementations;

internal sealed class AppointmentNotificationService(
    IEmailService emailService,
    IRemoteCaller remoteCaller,
    IAppointmentRepository repository
) : IAppointmentNotificationService
{
    private const int NotificationMinutesBefore = 10;
    private const string MessageSubject = "Appointment";
    private const string MessageHtmlBody = "You have an appointment in 10 minutes";

    public async Task NotifyDoctorsAboutAppointment(CancellationToken cancellationToken = default)
    {
        var doctorsIds = await repository.GetUpcomingAppointmentsDoctorsIds(NotificationMinutesBefore, cancellationToken);
        var emails = await remoteCaller.GetDoctorsEmailsAsync(doctorsIds);
        foreach (var email in emails)
        {
            var message = new MessageModel()
            {
                Subject = MessageSubject,
                HtmlBody = MessageHtmlBody,
            };
            await emailService.SendMessage(email, message);
        }
    }
}
