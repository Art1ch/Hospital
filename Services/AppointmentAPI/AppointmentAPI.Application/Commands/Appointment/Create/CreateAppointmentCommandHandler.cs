using AppointmentAPI.Application.Contracts.Repository.Appointment;
using AppointmentAPI.Core.Entities;
using AutoMapper;
using MediatR;

namespace AppointmentAPI.Application.Commands.Appointment.Create;

internal sealed class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Unit>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IMapper _mapper;

    public CreateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateAppointmentCommand command, CancellationToken cancellationToken)
    {
        var appointment = _mapper.Map<AppointmentEntity>(command.Request);
        await _appointmentRepository.CreateAsync(appointment, cancellationToken);
        return Unit.Value;
    }
}
