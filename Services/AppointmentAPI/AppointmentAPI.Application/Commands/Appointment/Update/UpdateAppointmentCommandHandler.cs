using AppointmentAPI.Application.Contracts.Repository.Appointment;
using AppointmentAPI.Core.Entities;
using AutoMapper;
using MediatR;

namespace AppointmentAPI.Application.Commands.Appointment.Update;

internal sealed class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IAppointmentRepository _appointmentRepository;

    public UpdateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateAppointmentCommand command, CancellationToken cancellationToken)
    {
        var appointment = _mapper.Map<AppointmentEntity>(command.Request);
        await _appointmentRepository.UpdateAsync(appointment);
        return Unit.Value;
    }
}
