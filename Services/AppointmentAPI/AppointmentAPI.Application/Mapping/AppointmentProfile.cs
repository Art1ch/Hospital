using AppointmentAPI.Application.RepositoryResults.Appointment;
using AppointmentAPI.Application.Requests.Appointment;
using AppointmentAPI.Application.Responses.Appointment;
using AppointmentAPI.Core.Entities;
using AutoMapper;

namespace AppointmentAPI.Application.Mapping;

public sealed class AppointmentProfile : Profile
{
    public AppointmentProfile()
    {
        CreateMap<CreateAppointmentRequest, AppointmentEntity>();
        CreateMap<UpdateAppointmentRequest, AppointmentEntity>();

        CreateMap<GetDoctorsAppointmentScheduleResult, GetDoctorsAppointmentScheduleResponse>()
            .ConstructUsing(src => new GetDoctorsAppointmentScheduleResponse(src));
    }
}
