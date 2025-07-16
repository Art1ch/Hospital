using AppointmentAPI.Notifications.Interfaces;
using Microsoft.Extensions.Hosting;

namespace AppointmentAPI.BackgroundWorkers.BackgroundWorkers;

internal class AppointmentReminderWorker : BackgroundService
{
    private readonly IAppointmentNotificationService _notifier;
    private const int DelayMinutes = 5;

    public AppointmentReminderWorker(IAppointmentNotificationService notifier)
    {
        _notifier = notifier;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _notifier.NotifyAboutAppointment();
            }
            catch (Exception)
            {
                throw;
            }
            await Task.Delay(TimeSpan.FromMinutes(DelayMinutes), stoppingToken);
        }
    }
}
