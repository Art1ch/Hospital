using AppointmentAPI.Infrastructure.Interfaces;
using AppointmentAPI.Infrastructure.Models;

namespace AppointmentAPI.Infrastructure.Implementations;

internal class EmailSender : IEmailSender
{
    public Task SendMessageAsync(string from, string to, MessageModel message)
    {
        throw new NotImplementedException();
    }
}
