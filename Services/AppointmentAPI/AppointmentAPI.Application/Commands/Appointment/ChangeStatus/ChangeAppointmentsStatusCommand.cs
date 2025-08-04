using AppointmentAPI.Application.Abstractions;
using AppointmentAPI.Application.Requests.Appointment;
using MediatR;

namespace AppointmentAPI.Application.Commands.Appointment.ChangeStatus;

public sealed record ChangeAppointmentsStatusCommand(
    ChangeAppointmentsStatusRequest Request
) : BaseRequest<ChangeAppointmentsStatusRequest, Unit>(Request);
