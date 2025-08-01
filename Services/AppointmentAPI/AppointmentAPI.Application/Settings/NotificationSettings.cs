namespace AppointmentAPI.Application.Settings;

public sealed class NotificationSettings
{
    public int CheckIntervalMinutes { get; set; }
    public int ToleranceMinutes { get; set; }
    public List<NotificationConfig> NotificationConfigs { get; set; }
}
