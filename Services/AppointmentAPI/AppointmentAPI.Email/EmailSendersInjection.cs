using AppointmentAPI.Email.Implementations;
using AppointmentAPI.Email.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentAPI.Email;

public static class EmailSendersInjection
{
    public static void AddEmailSenders(this IServiceCollection services)
    {
        services.AddScoped<IEmailSender, EmailSender>();
    }
}
