using AppointmentAPI.Core.Enums;

namespace AppointmentAPI.Application.Requests.Appointment;

public record ChangeAppointmentsStatusRequest(
    Guid Id,
    AppointmentStatus Status);