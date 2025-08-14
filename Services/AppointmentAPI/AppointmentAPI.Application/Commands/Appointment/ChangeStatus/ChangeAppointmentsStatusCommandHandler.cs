using AppointmentAPI.Application.Contracts.Repository.Appointment;
using MediatR;

namespace AppointmentAPI.Application.Commands.Appointment.ChangeStatus;

internal sealed class ChangeAppointmentsStatusCommandHandler(
    IAppointmentRepository appointmentRepository
) : IRequestHandler<ChangeAppointmentsStatusCommand, Unit>
{
    public async Task<Unit> Handle(ChangeAppointmentsStatusCommand command, CancellationToken cancellationToken)
    {
        var id = command.Request.Id;
        var status = command.Request.Status;
        await appointmentRepository.ChangeAppointmentStatusAsync(id, status, cancellationToken);
        return Unit.Value;
    }
}
