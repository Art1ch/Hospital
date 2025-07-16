namespace AppointmentAPI.Email.Models;

public class MessageModel
{
    public string Subject { get; set; } = string.Empty;
    public string HtmlBody { get; set; } = string.Empty;
}
