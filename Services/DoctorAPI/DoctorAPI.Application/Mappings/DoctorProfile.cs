using AutoMapper;
using DoctorAPI.Application.Dto_s.RepoDto_s.Doctor.Create;
using DoctorAPI.Application.Dto_s.RepoDto_s.Doctor.Delete;
using DoctorAPI.Application.Dto_s.RepoDto_s.Doctor.Update;
using DoctorAPI.Application.WebDto_s.Doctor.Delete;
using DoctorAPI.Application.WebDto_s.Doctor.GetAll;
using DoctorAPI.Application.WebDto_s.Doctor.GetById;
using DoctorAPI.Application.WebDto_s.Doctor.GetBySpecialization;
using DoctorAPI.Application.WebDto_s.Doctor.GetByStatus;
using DoctorAPI.Application.WebDto_s.Doctor.Update;
using DoctorAPI.Application.WebRequests.Doctor.Create;
using DoctorAPI.Application.WebRequests.Doctor.Update;
using DoctorAPI.Core.Entities;

namespace DoctorAPI.Application.Mappings;

public class DoctorProfile : Profile
{
    public DoctorProfile()
    {
        CreateMap<CreateDoctorRequestDto,
            DoctorEntity>()
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
            opt => opt.Ignore())
            .AfterMap((src, dest, context) =>
            {
                dest.Specialization = new SpecializationEntity()
                {
                    Name = src.SpecializationName
                };
            });

        CreateMap<CreateDoctorRepoDto,
            CreateDoctorResponseDto>()
            .ForMember(dest => dest.Id,
            opt => opt.MapFrom(d => d.Id));

        CreateMap<UpdateDoctorRequestDto,
            DoctorEntity>()
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
            opt => opt.Ignore())
            .AfterMap((src, dest, context) =>
            {
                dest.Specialization = new SpecializationEntity()
                {
                    Name = src.SpecializationName
                };
            });

        CreateMap<UpdateDoctorRepoDto,
            UpdateDoctorResponseDto>()
            .ForMember(dest => dest.Id,
            opt => opt.MapFrom(d => d.Id));

        CreateMap<DeleteDoctorRepoDto,
            DeleteDoctorResponseDto>()
            .ConstructUsing(src => 
            new DeleteDoctorResponseDto());

        CreateMap<List<GetAllDoctorsRepoDto>,
            GetAllDoctorsResponseDto>()
            .ConstructUsing(src => 
            new GetAllDoctorsResponseDto(src));

        CreateMap<GetBySpecializationRepoDto,
            GetBySpecializationDoctorResponseDto>()
            .ConstructUsing(src => 
            new GetBySpecializationDoctorResponseDto(src));

        CreateMap<List<GetByStatusRepoDto>,
            GetByStatusDoctorsResponseDto>()
            .ConstructUsing(src =>
            new GetByStatusDoctorsResponseDto(src));

        CreateMap<GetByIdDoctorRepoDto,
            GetByIdDoctorResponseDto>()
            .ConstructUsing(src =>
            new GetByIdDoctorResponseDto(src));
    }
}
