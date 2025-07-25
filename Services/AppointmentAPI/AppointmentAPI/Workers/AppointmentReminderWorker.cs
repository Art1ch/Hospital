using AppointmentAPI.Application.Contracts.AppointmentNotificationService;
using NCrontab;

namespace AppointmentAPI.Workers.AppointmentReminderWorker;

internal class AppointmentReminderWorker : BackgroundService
{
    private const string CronExpression = "*/5 * * * *";
    private readonly IAppointmentNotificationService _notifier;
    private readonly CrontabSchedule _schedule;
    private DateTime _nextRun;

    public AppointmentReminderWorker(IAppointmentNotificationService notifier)
    {
        _notifier = notifier;
        _schedule = CrontabSchedule.Parse(CronExpression);
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
