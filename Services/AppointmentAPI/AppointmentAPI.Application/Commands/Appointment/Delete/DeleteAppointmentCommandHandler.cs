using AppointmentAPI.Application.Contracts.Repository.Appointment;
using MediatR;

namespace AppointmentAPI.Application.Commands.Appointment.Delete;

internal sealed class DeleteAppointmentCommandHandler(
     IAppointmentRepository appointmentRepository
) : IRequestHandler<DeleteAppointmentCommand, Unit>
{
    public async Task<Unit> Handle(DeleteAppointmentCommand command, CancellationToken cancellationToken)
    {
        await appointmentRepository.DeleteAsync(command.Id, cancellationToken);
        return Unit.Value;
    }
}
