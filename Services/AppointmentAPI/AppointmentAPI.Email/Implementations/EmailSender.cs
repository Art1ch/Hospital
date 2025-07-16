using AppointmentAPI.Email.Interfaces;
using AppointmentAPI.Email.Models;

namespace AppointmentAPI.Email.Implementations;

internal class EmailSender : IEmailSender
{
    public Task SendMessageAsync(string from, string to, MessageModel message)
    {
        throw new NotImplementedException();
    }
}
