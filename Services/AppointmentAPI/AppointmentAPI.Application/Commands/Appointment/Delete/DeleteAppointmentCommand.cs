using AppointmentAPI.Application.Abstractions;
using AppointmentAPI.Application.Requests.Appointment;
using MediatR;

namespace AppointmentAPI.Application.Commands.Appointment.Delete;

public sealed record DeleteAppointmentCommand(
    DeleteAppointmentRequest Request
) : BaseRequest<DeleteAppointmentRequest, Unit>(Request);
