using AppointmentAPI.Application.Contracts.Email;
using AppointmentAPI.Application.Models;
using AppointmentAPI.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace AppointmentAPI.Infrastructure.Services.Email;

internal class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailService(IOptions<SmtpSettings> smtpOptions)
    {
        _smtpSettings = smtpOptions.Value;
    }

    public async Task SendMessage(string receiversEmail, MessageModel messageModel)
    {
        using var client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port)
        {
            Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
        };

        var message = new MailMessage
        {
            From = new MailAddress(_smtpSettings.FromAddress, _smtpSettings.FromName),
            Subject = messageModel.Subject,
            Body = messageModel.HtmlBody,
            IsBodyHtml = true,
        };

        message.To.Add(receiversEmail);

        await client.SendMailAsync(message);
    }
}
