namespace AppointmentAPI.Application.Validation.Constants;

internal static class AppointmentConstants
{
    public static readonly TimeOnly StartAppointmentTime = new TimeOnly(7, 0, 0);
    public static readonly TimeOnly EndAppointmentTime = new TimeOnly(18, 0, 0);
    public const string StartAppointmentTimeString = "7:00";
    public const string EndAppointmentTimeString = "18:00";
}
