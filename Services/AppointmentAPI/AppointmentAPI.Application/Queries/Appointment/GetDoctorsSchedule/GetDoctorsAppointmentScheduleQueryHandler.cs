using AppointmentAPI.Application.Contracts.Repository.Appointment;
using AppointmentAPI.Application.Responses.Appointment;
using AutoMapper;
using MediatR;

namespace AppointmentAPI.Application.Queries.Appointment.GetDoctorsSchedule;

internal sealed class GetDoctorsAppointmentScheduleQueryHandler(
    IAppointmentRepository appointmentRepository,
    IMapper mapper
) : IRequestHandler<GetDoctorsAppointmentScheduleQuery, GetDoctorsAppointmentScheduleResponse>
{
    public async Task<GetDoctorsAppointmentScheduleResponse> Handle(GetDoctorsAppointmentScheduleQuery query, CancellationToken cancellationToken)
    {
        var result = await appointmentRepository.GetDoctorsAppointmentScheduleAsync(query.DoctorId, cancellationToken);
        var response = mapper.Map<GetDoctorsAppointmentScheduleResponse>(result);
        return response;
    }
}