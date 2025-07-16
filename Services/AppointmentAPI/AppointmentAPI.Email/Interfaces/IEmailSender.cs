using AppointmentAPI.Email.Models;

namespace AppointmentAPI.Email.Interfaces;

public interface IEmailSender
{
    Task SendMessageAsync(string from, string to, MessageModel message);
}
