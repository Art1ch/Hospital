using AppointmentAPI.Application.Contracts.Repository.Appointment;
using AppointmentAPI.Core.Entities;
using AutoMapper;
using MediatR;

namespace AppointmentAPI.Application.Commands.Appointment.Create;

internal sealed class CreateAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository,
    IMapper mapper
) : IRequestHandler<CreateAppointmentCommand, Unit>
{
    public async Task<Unit> Handle(CreateAppointmentCommand command, CancellationToken cancellationToken)
    {
        var appointment = mapper.Map<AppointmentEntity>(command.Request);
        await appointmentRepository.CreateAsync(appointment, cancellationToken);
        return Unit.Value;
    }
}
