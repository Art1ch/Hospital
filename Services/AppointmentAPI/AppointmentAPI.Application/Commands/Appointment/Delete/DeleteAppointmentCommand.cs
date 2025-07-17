using AppointmentAPI.Application.Abstractions;
using MediatR;

namespace AppointmentAPI.Application.Commands.Appointment.Delete;

public sealed record DeleteAppointmentCommand(
    Guid Id
) : BaseRequest<Guid, Unit>(Id);
