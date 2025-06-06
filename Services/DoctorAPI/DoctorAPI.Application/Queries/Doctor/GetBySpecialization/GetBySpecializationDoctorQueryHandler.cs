using AutoMapper;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.GetById;
using DoctorAPI.Application.WebDto_s.Doctor.GetBySpecialization;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetBySpecialization;

public class GetBySpecializationDoctorQueryHandler<TId1, TId2>
    : IRequestHandler<GetBySpecializationDoctorQuery<TId1, TId2>,
        GetBySpecializationDoctorResponseDto<TId1, TId2>>
{
    private readonly IDoctorRepository<TId1, TId2> _doctorRepository;
    private readonly IMapper _mapper;

    public GetBySpecializationDoctorQueryHandler(
        IDoctorRepository<TId1, TId2> doctorRepository, 
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<GetBySpecializationDoctorResponseDto<TId1, TId2>> Handle(
        GetBySpecializationDoctorQuery<TId1, TId2> request,
        CancellationToken ct)
    {
        var doctor = await _doctorRepository.GetBySpecialization(request.Dto.SpecializationId, ct);
        var response = _mapper.Map<GetBySpecializationDoctorResponseDto<TId1, TId2>>(doctor);
        return response;
    }
}
