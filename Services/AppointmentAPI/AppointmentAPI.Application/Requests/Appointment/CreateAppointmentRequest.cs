namespace AppointmentAPI.Application.Requests.Appointment;

public sealed record CreateAppointmentRequest(
    Guid DoctorId,
    DateOnly Date,
    TimeOnly StartAppointmentTime,
    TimeOnly EndAppointmentTime
);