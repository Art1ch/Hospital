using AutoMapper;
using DoctorAPI.Application.RepositoryResults.Doctor.GetAll;
using DoctorAPI.Application.RepositoryResults.Doctor.GetById;
using DoctorAPI.Application.RepositoryResults.Doctor.GetBySpecialization;
using DoctorAPI.Application.RepositoryResults.Doctor.GetByStatus;
using DoctorAPI.Application.Requests.Doctor;
using DoctorAPI.Application.Responses.Doctor;
using DoctorAPI.Application.Entities;

namespace DoctorAPI.Application.Mappings;

internal class DoctorProfile : Profile
{
    public DoctorProfile()
    {
        CreateMap<CreateDoctorRequest, DoctorEntity>()
            .AfterMap((src, dest, context) =>
            {
                dest.Specialization = new SpecializationEntity()
                {
                    Name = src.SpecializationName
                };
            });


        CreateMap<UpdateDoctorRequest, DoctorEntity>()
            .AfterMap((src, dest, context) =>
            {
                dest.Specialization = new SpecializationEntity()
                {
                    Name = src.SpecializationName
                };
            });

        CreateMap<List<GetAllDoctorsResult>, GetAllDoctorsResponse>()
            .ConstructUsing(src => new GetAllDoctorsResponse(src));

        CreateMap<GetDoctorBySpecializationResult,GetBySpecializationDoctorResponse>()
            .ConstructUsing(src => new GetBySpecializationDoctorResponse(src));

        CreateMap<List<GetDoctorsByStatusResult>, GetByStatusDoctorsResponse>()
            .ConstructUsing(src => new GetByStatusDoctorsResponse(src));

        CreateMap<GetDoctorInfoByIdResult, GetByIdDoctorResponse>()
            .ConstructUsing(src => new GetByIdDoctorResponse(src));
    }
}
