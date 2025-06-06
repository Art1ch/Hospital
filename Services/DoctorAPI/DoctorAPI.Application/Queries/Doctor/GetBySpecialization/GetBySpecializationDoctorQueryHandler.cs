using AutoMapper;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.GetBySpecialization;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetBySpecialization;

public class GetBySpecializationDoctorQueryHandler<TDoctorId,
    TSpecializationId>
    : IRequestHandler<GetBySpecializationDoctorQuery<TDoctorId,
        TSpecializationId>,
        GetBySpecializationDoctorResponseDto<TDoctorId, TSpecializationId>>
{
    private readonly IDoctorRepository<TDoctorId, TSpecializationId> _doctorRepository;
    private readonly IMapper _mapper;

    public GetBySpecializationDoctorQueryHandler(
        IDoctorRepository<TDoctorId,
            TSpecializationId> doctorRepository, 
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<GetBySpecializationDoctorResponseDto<TDoctorId, TSpecializationId>> Handle(
        GetBySpecializationDoctorQuery<TDoctorId, TSpecializationId> request,
        CancellationToken ct)
    {
        var doctor = await _doctorRepository.GetBySpecializationAsync(
            request.Dto.SpecializationId, ct);
        var response = _mapper.Map
            <GetBySpecializationDoctorResponseDto<TDoctorId, TSpecializationId>>(doctor);
        return response;
    }
}
