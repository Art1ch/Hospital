using AppointmentAPI.Application.Abstractions;
using AppointmentAPI.Application.Requests.Appointment;
using MediatR;

namespace AppointmentAPI.Application.Commands.Appointment.Create;

public sealed record CreateAppointmentCommand(
    CreateAppointmentRequest Request) : BaseRequest<CreateAppointmentRequest, Unit>(Request);
