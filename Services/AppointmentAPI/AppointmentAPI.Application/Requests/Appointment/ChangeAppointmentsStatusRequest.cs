using AppointmentAPI.Core.Enums;

namespace AppointmentAPI.Application.Requests.Appointment;

public sealed record ChangeAppointmentsStatusRequest(
    Guid Id,
    AppointmentStatus Status
);