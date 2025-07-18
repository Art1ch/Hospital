using AppointmentAPI.Application.Contracts.AppointmentNotificationService;

namespace AppointmentAPI.Workers.AppointmentReminderWorker;

internal class AppointmentReminderWorker(
    IAppointmentNotificationService notifier
) : BackgroundService
{
    private const int DelayMinutes = 5;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested)
        {
            try
            {
                await notifier.NotifyDoctorsAboutAppointment(stoppingToken);
            }
            catch (Exception)
            {
                throw;
            }
            await Task.Delay(TimeSpan.FromMinutes(DelayMinutes), stoppingToken);
        }
    }
}
