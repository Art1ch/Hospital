using AppointmentAPI.Application.Contracts.Repository.Appointment;
using MediatR;

namespace AppointmentAPI.Application.Commands.Appointment.Delete;

internal sealed class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, Unit>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public DeleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<Unit> Handle(DeleteAppointmentCommand command, CancellationToken cancellationToken)
    {
        await _appointmentRepository.DeleteAsync(command.Id, cancellationToken);
        return Unit.Value;
    }
}
