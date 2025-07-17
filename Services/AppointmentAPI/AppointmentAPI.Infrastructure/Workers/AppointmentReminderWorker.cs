using AppointmentAPI.Infrastructure.Interfaces;
using Microsoft.Extensions.Hosting;

namespace AppointmentAPI.Infrastructure.BackgroundWorkers;

internal class AppointmentReminderWorker : BackgroundService
{
    private const int DelayMinutes = 5;
    private readonly IAppointmentNotificationService _notifier;

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
