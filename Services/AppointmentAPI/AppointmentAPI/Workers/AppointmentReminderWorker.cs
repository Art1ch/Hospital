using AppointmentAPI.Application.Contracts.AppointmentNotificationService;
using AppointmentAPI.Application.Settings;
using Microsoft.Extensions.Options;
using NCrontab;

namespace AppointmentAPI.Workers.AppointmentReminderWorker;

internal class AppointmentReminderWorker : BackgroundService
{
    private readonly string _cronExpression = "*/5 * * * *";
    private readonly IAppointmentNotificationService _notifier;
    private readonly CrontabSchedule _schedule;
    private DateTime _nextRun;

    public AppointmentReminderWorker(IAppointmentNotificationService notifier, IOptions<NotificationSettings> settings)
    {
        _notifier = notifier;
        _cronExpression = $"*/{settings.Value.CheckIntervalMinutes} * * * *";
        _schedule = CrontabSchedule.Parse(_cronExpression);
        _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            if (now > _nextRun)
            {
                try
                {
                    await _notifier.NotifyDoctorsAboutAppointment(stoppingToken);
                }
                catch (Exception ex)
                {
                    throw;
                }
                _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            }
            var delay = _nextRun - DateTime.Now;
            if (delay > TimeSpan.Zero)
            {
                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}
