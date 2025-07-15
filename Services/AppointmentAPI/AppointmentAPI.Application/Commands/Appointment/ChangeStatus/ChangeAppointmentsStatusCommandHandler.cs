using AppointmentAPI.Application.Contracts.Repository.Appointment;
using MediatR;

namespace AppointmentAPI.Application.Commands.Appointment.ChangeStatus;

internal sealed class ChangeAppointmentsStatusCommandHandler : IRequestHandler<ChangeAppointmentsStatusCommand, Unit>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public ChangeAppointmentsStatusCommandHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<Unit> Handle(ChangeAppointmentsStatusCommand command, CancellationToken cancellationToken)
    {
        var id = command.Request.Id;
        var status = command.Request.Status;
        await _appointmentRepository.ChangeAppointmentStatusAsync(id, status, cancellationToken);
        return Unit.Value;
    }
}
