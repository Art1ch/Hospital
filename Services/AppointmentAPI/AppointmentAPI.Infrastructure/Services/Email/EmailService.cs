using AppointmentAPI.Application.Contracts.Email;
using AppointmentAPI.Application.Models;

namespace AppointmentAPI.Infrastructure.Services.Email;

internal class EmailService : IEmailService
{
    public Task SendMessage(string to, MessageModel message)
    {
        throw new NotImplementedException();
    }
}
