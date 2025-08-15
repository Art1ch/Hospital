using AppointmentAPI.Application.Contracts.Repository.Appointment;
using AppointmentAPI.Core.Entities;
using AutoMapper;
using MediatR;

namespace AppointmentAPI.Application.Commands.Appointment.Update;

internal sealed class UpdateAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository,
    IMapper mapper
) : IRequestHandler<UpdateAppointmentCommand, Unit>
{
    public async Task<Unit> Handle(UpdateAppointmentCommand command, CancellationToken cancellationToken)
    {
        var appointment = mapper.Map<AppointmentEntity>(command.Request);
        await appointmentRepository.UpdateAsync(appointment);
        return Unit.Value;
    }
}
