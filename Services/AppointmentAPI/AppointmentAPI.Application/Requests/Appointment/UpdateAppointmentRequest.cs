namespace AppointmentAPI.Application.Requests.Appointment;

public sealed record UpdateAppointmentRequest(
    Guid Id,
    DateOnly Date,
    TimeOnly StartAppointmentTime,
    TimeOnly EndAppointmentTime
);
