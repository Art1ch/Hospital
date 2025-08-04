using AppointmentAPI.Application.Models;

namespace AppointmentAPI.Application.Contracts.Email;

public interface IEmailService
{
    Task SendMessage(string receiversEmail, MessageModel message);
}
