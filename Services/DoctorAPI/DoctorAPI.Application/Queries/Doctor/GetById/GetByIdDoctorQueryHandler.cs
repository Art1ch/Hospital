using AutoMapper;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.GetById;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetById;

public class GetByIdDoctorQueryHandler<TDoctorId,
    TSpecializationId>
    : IRequestHandler<GetByIdDoctorQuery<TDoctorId,
        TSpecializationId>,
        GetByIdDoctorResponseDto<TDoctorId,
            TSpecializationId>>
{
    private readonly IDoctorRepository<TDoctorId,
        TSpecializationId> _doctorRepository;
    private readonly IMapper _mapper;
    public GetByIdDoctorQueryHandler(
        IDoctorRepository<TDoctorId,
            TSpecializationId> doctorRepository,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }
    public async Task<GetByIdDoctorResponseDto<TDoctorId, TSpecializationId>> Handle(
        GetByIdDoctorQuery<TDoctorId, TSpecializationId> request,
        CancellationToken ct)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.Dto.Id, ct);
        var response = _mapper.Map<GetByIdDoctorResponseDto<TDoctorId, TSpecializationId>>(doctor);
        return response;
    }
}
