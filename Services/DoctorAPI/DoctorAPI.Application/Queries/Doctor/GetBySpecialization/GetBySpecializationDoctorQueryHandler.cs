using AutoMapper;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.GetBySpecialization;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetBySpecialization;

public class GetBySpecializationDoctorQueryHandler
    : IRequestHandler<GetBySpecializationDoctorQuery,
        GetBySpecializationDoctorResponseDto>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public GetBySpecializationDoctorQueryHandler(
        IDoctorRepository doctorRepository, 
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<GetBySpecializationDoctorResponseDto> Handle(
        GetBySpecializationDoctorQuery request,
        CancellationToken ct)
    {
        var doctorInfo = await _doctorRepository.GetBySpecializationAsync(
            request.Dto.SpecializationId, ct);
        var response = _mapper.Map
            <GetBySpecializationDoctorResponseDto>(doctorInfo);
        return response;
    }
}
