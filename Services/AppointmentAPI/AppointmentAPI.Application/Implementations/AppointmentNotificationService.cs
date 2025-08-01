using AppointmentAPI.Application.Contracts.Email;
using AppointmentAPI.Application.Contracts.RemoteCaller;
using AppointmentAPI.Application.Contracts.Repository.Appointment;
using AppointmentAPI.Application.Models;
using AppointmentAPI.Application.Contracts.AppointmentNotificationService;
using Microsoft.Extensions.Options;
using AppointmentAPI.Application.Settings;

namespace AppointmentAPI.Application.Implementations;

internal sealed class AppointmentNotificationService(
    IEmailService emailService,
    IRemoteCaller remoteCaller,
    IAppointmentRepository repository,
    IOptions<NotificationSettings> options
) : IAppointmentNotificationService
{
    public async Task NotifyDoctorsAboutAppointment(CancellationToken cancellationToken = default)
    {
        var toleranceMinutes = options.Value.ToleranceMinutes;
        var configs = options.Value.NotificationConfigs;

        foreach (var config in configs)
        {
            var doctorsIds = await repository.GetUpcomingAppointmentsDoctorsIds(config.MinutesBefore, toleranceMinutes, cancellationToken);
            var emails = await remoteCaller.GetDoctorsEmailsAsync(doctorsIds);
            await SendNotifications(emails, config);
        }
    }

    private async Task SendNotifications(IEnumerable<string> emails, NotificationConfig config)
    {
        foreach (var email in emails)
        {
            var message = new MessageModel()
            {
                Subject = config.Subject,
                HtmlBody = config.Body,
            };

            await emailService.SendMessage(email, message);
        }
    }
}
