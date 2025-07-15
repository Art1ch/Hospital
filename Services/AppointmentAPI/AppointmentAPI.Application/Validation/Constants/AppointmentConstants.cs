namespace AppointmentAPI.Application.Validation.Constants;

internal static class AppointmentConstants
{
    public static TimeOnly StartAppointmentTime { get; private set; } = new TimeOnly(7, 0, 0);
    public static TimeOnly EndAppointmentTime { get; private set; } = new TimeOnly(18, 0, 0);
    public const string StartAppointmentTimeString = "7:00";
    public const string EndAppointmentTimeString = "18:00";
}
