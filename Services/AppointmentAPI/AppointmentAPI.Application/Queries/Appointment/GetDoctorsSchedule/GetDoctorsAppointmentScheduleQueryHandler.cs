using AppointmentAPI.Application.Contracts.Repository.Appointment;
using AppointmentAPI.Application.Responses.Appointment;
using AutoMapper;
using MediatR;

namespace AppointmentAPI.Application.Queries.Appointment.GetDoctorsSchedule;

internal sealed class GetDoctorsAppointmentScheduleQueryHandler :
    IRequestHandler<GetDoctorsAppointmentScheduleQuery, GetDoctorsAppointmentScheduleResponse>
{
    private readonly IMapper _mapper;
    private readonly IAppointmentRepository _appointmentRepository;
    public GetDoctorsAppointmentScheduleQueryHandler(IMapper mapper, IAppointmentRepository appointmentRepository)
    {
        _mapper = mapper;
        _appointmentRepository = appointmentRepository;
    }

    public async Task<GetDoctorsAppointmentScheduleResponse> Handle(GetDoctorsAppointmentScheduleQuery query, CancellationToken cancellationToken)
    {
        var result = await _appointmentRepository.GetDoctorsAppointmentScheduleAsync(query.DoctorId, cancellationToken);
        var response = _mapper.Map<GetDoctorsAppointmentScheduleResponse>(result);
        return response;
    }
}