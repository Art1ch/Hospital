using AutoMapper;
using DoctorAPI.Application.RepositoryResults.Doctor.GetAll;
using DoctorAPI.Application.RepositoryResults.Doctor.GetById;
using DoctorAPI.Application.RepositoryResults.Doctor.GetBySpecialization;
using DoctorAPI.Application.RepositoryResults.Doctor.GetByStatus;
using DoctorAPI.Application.Requests.Doctor;
using DoctorAPI.Application.Responses.Doctor;
using DoctorAPI.Core.Entities;

namespace DoctorAPI.Application.Mappings;

internal class DoctorProfile : Profile
{
    public DoctorProfile()
    {
        CreateMap<CreateDoctorRequest,
            DoctorEntity>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(d => d.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(d => d.LastName))
            .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(d => d.MiddleName))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(d => d.Status))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(d => d.BirthDate))
            .ForMember(dest => dest.CareerStartDay, opt => opt.MapFrom(d => d.CareerStartDay))
            .ForMember(dest => dest.Specialization, opt => opt.Ignore())
            .AfterMap((src, dest, context) =>
            {
                dest.Specialization = new SpecializationEntity()
                {
                    Name = src.SpecializationName
                };
            });


        CreateMap<UpdateDoctorRequest, DoctorEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(d => d.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(d => d.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(d => d.LastName))
            .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(d => d.MiddleName))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(d => d.Status))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(d => d.BirthDate))
            .ForMember(dest => dest.CareerStartDay, opt => opt.MapFrom(d => d.CareerStartDay))
            .ForMember(dest => dest.Specialization, opt => opt.Ignore())
            .AfterMap((src, dest, context) =>
            {
                dest.Specialization = new SpecializationEntity()
                {
                    Name = src.SpecializationName
                };
            });

        CreateMap<List<GetAllDoctorsResult>, GetAllDoctorsResponse>()
            .ConstructUsing(src =>  new GetAllDoctorsResponse(src));

        CreateMap<GetBySpecializationResult,GetBySpecializationDoctorResponse>()
            .ConstructUsing(src => new GetBySpecializationDoctorResponse(src));

        CreateMap<List<GetByStatusResult>, GetByStatusDoctorsResponse>()
            .ConstructUsing(src => new GetByStatusDoctorsResponse(src));

        CreateMap<GetByIdDoctorResult, GetByIdDoctorResponse>()
            .ConstructUsing(src => new GetByIdDoctorResponse(src));
    }
}
