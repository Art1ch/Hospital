using AppointmentAPI.Infrastructure.Models;

namespace AppointmentAPI.Infrastructure.Interfaces;

public interface IEmailSender
{
    Task SendMessageAsync(string from, string to, MessageModel message);
}
