using AppointmentAPI.Application.Abstractions;
using AppointmentAPI.Application.Requests.Appointment;
using MediatR;

namespace AppointmentAPI.Application.Commands.Appointment.Update;

public sealed record UpdateAppointmentCommand(
    UpdateAppointmentRequest Request) : BaseRequest<UpdateAppointmentRequest, Unit>(Request);
