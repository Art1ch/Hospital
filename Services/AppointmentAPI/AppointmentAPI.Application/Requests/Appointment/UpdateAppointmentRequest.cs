namespace AppointmentAPI.Application.Requests.Appointment;

public record UpdateAppointmentRequest(
    Guid Id,
    DateOnly Date,
    TimeOnly StartAppointmentTime,
    TimeOnly EndAppointmentTime);
