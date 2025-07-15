using AppointmentAPI.Application.Abstractions;
namespace AppointmentAPI.Application.Requests.Appointment;

public record CreateAppointmentRequest(
    Guid DoctorId,
    DateOnly Date,
    TimeOnly StartAppointmentTime,
    TimeOnly EndAppointmentTime);