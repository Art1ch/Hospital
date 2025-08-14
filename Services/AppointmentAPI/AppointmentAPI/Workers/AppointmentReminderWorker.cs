using AppointmentAPI.Application.Contracts.AppointmentNotificationService;
using AppointmentAPI.Application.Settings;
using Microsoft.Extensions.Options;
using NCrontab;

namespace AppointmentAPI.Workers.AppointmentReminderWorker;

internal class AppointmentReminderWorker : BackgroundService
{
    private readonly string _cronExpression;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly CrontabSchedule _schedule;
    private DateTime _nextRun;

    public AppointmentReminderWorker(IServiceScopeFactory scopeFactory, IOptions<NotificationSettings> settings)
    {
        _scopeFactory = scopeFactory;
        _cronExpression = "*/1 * * * *"; ;
        //_cronExpression = $"*/{settings.Value.CheckIntervalMinutes} * * * *";
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
                using (var scope = _scopeFactory.CreateScope())
                {
                    var notifier = scope.ServiceProvider
                        .GetRequiredService<IAppointmentNotificationService>();

                    await notifier.NotifyDoctorsAboutAppointment(stoppingToken);
                }
                _nextRun = _schedule.GetNextOccurrence(now);
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
