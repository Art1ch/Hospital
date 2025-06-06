using AutoMapper;
using DoctorAPI.Application.WebDto_s.Doctor.GetAll;
using DoctorAPI.Application.WebDto_s.Doctor.GetById;
using DoctorAPI.Application.WebDto_s.Doctor.GetByStatus;
using DoctorAPI.Application.WebDto_s.Doctor.Update;
using DoctorAPI.Application.WebRequests.Doctor.Create;
using DoctorAPI.Application.WebRequests.Doctor.Update;
using DoctorAPI.Core.Entities;

namespace DoctorAPI.Application.Mappings;

public class DoctorProfle<TDoctorId, TSpecializationId> : Profile
{
    public DoctorProfle()
    {
        CreateMap<CreateDoctorRequestDto<TDoctorId, TSpecializationId>,
            DoctorEntity<TDoctorId, TSpecializationId>>()
            .ForMember(dest => dest.Id,
            opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.FirstName,
            opt => opt.MapFrom(d => d.FirstName))
            .ForMember(dest => dest.LastName,
            opt => opt.MapFrom(d => d.LastName))
            .ForMember(dest => dest.MiddleName,
            opt => opt.MapFrom(d => d.MiddleName))
            .ForMember(dest => dest.Status,
            opt => opt.MapFrom(d => d.Status))
            .ForMember(dest => dest.BirthDate,
            opt => opt.MapFrom(d => d.BirthDate))
            .ForMember(dest => dest.CareerStartDay,
            opt => opt.MapFrom(d => d.CareerStartDay))
            .ForMember(dest => dest.Specialization,
            opt => opt.MapFrom(d => d.Specialization));

        CreateMap<DoctorEntity<TDoctorId, TSpecializationId>,
            CreateDoctorResponseDto<TDoctorId>>()
            .ForMember(dest => dest.Id,
            opt => opt.MapFrom(d => d.Id));

        CreateMap<UpdateDoctorRequestDto<TDoctorId, TSpecializationId>,
            DoctorEntity<TDoctorId, TSpecializationId>>()
            .ForMember(dest => dest.Id,
            opt => opt.MapFrom(d => d.Id))
            .ForMember(dest => dest.FirstName,
            opt => opt.MapFrom(d => d.FirstName))
            .ForMember(dest => dest.LastName,
            opt => opt.MapFrom(d => d.LastName))
            .ForMember(dest => dest.MiddleName,
            opt => opt.MapFrom(d => d.MiddleName))
            .ForMember(dest => dest.Status,
            opt => opt.MapFrom(d => d.Status))
            .ForMember(dest => dest.BirthDate,
            opt => opt.MapFrom(d => d.BirthDate))
            .ForMember(dest => dest.CareerStartDay,
            opt => opt.MapFrom(d => d.CareerStartDay))
            .ForMember(dest => dest.Specialization,
            opt => opt.MapFrom(d => d.Specialization));

        CreateMap<DoctorEntity<TDoctorId, TSpecializationId>,
            UpdateDoctorResponseDto<TDoctorId>>()
            .ForMember(dest => dest.Id,
            opt => opt.MapFrom(d => d.Id));

        CreateMap<List<DoctorEntity<TDoctorId, TSpecializationId>>,
            GetAllDoctorsResponseDto<TDoctorId, TSpecializationId>>()
            .ForMember(dest => dest.Doctors,
            opt => opt.MapFrom(d => d));

        CreateMap<List<DoctorEntity<TDoctorId, TSpecializationId>>,
            GetByStatusDoctorsResponseDto<TDoctorId, TSpecializationId>>()
            .ForMember(dest => dest.Doctors,
            opt => opt.MapFrom(d => d));

        CreateMap<DoctorEntity<TDoctorId, TSpecializationId>,
            GetByIdDoctorResponseDto<TDoctorId, TSpecializationId>>()
            .ForMember(dest => dest.Doctor,
            opt => opt.MapFrom(d => d));
    }
}
