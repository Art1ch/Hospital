using AppointmentAPI.Application.Contracts.Repository.Appointment;
using MediatR;

namespace AppointmentAPI.Application.Commands.Appointment.Delete;

internal sealed class DeleteAppointmentCommandHandler(
     IAppointmentRepository appointmentRepository
) : IRequestHandler<DeleteAppointmentCommand, Unit>
{
    public async Task<Unit> Handle(DeleteAppointmentCommand command, CancellationToken cancellationToken)
    {
        var id = command.Request.Id;
        await appointmentRepository.DeleteAsync(id, cancellationToken);
        return Unit.Value;
    }
}
