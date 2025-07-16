using AppointmentAPI.BackgroundWorkers.BackgroundWorkers;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentAPI.BackgroundWorkers;

public static class BackgroundWorkersInjection
{
    public static void AddBackgroundWorkers(this IServiceCollection services)
    {
        services.AddHostedService<AppointmentReminderWorker>();
    }
}
