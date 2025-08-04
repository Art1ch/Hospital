namespace AppointmentAPI.Application.Settings;

public sealed record NotificationConfig(
    int MinutesBefore,
    string Subject,
    string Body
);