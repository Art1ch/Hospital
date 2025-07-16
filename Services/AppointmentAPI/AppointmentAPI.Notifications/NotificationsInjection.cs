using AppointmentAPI.Notifications.Implementations;
using AppointmentAPI.Notifications.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentAPI.Notifications;

public static class NotificationsInjection
{
    public static void AddNotifiers(this IServiceCollection services)
    {
        services.AddScoped<IAppointmentNotificationService, AppointmentEmailNotificationService>();
    }
}
