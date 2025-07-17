namespace AppointmentAPI.Core.Enums;

public enum AppointmentStatus : byte
{
    Unspecified = 0,
    Pending = 1,
    Cancelled = 2,
    Approved = 3,
}
