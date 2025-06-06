using AutoMapper;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.GetByStatus;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetByStatus;

public class GetByStatusDoctorsQueryHandler<TDoctorId,
    TSpecializationId>
    : IRequestHandler<GetByStatusDoctorsQuery<TDoctorId,
        TSpecializationId>,
        GetByStatusDoctorsResponseDto<TDoctorId,
            TSpecializationId>>
{
    private readonly IDoctorRepository<TDoctorId,
        TSpecializationId> _doctorRepository;
    private readonly IMapper _mapper;

    public GetByStatusDoctorsQueryHandler(
        IDoctorRepository<TDoctorId,
            TSpecializationId> doctorRepository,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<GetByStatusDoctorsResponseDto<TDoctorId, TSpecializationId>> Handle(
        GetByStatusDoctorsQuery<TDoctorId, TSpecializationId> request,
        CancellationToken ct)
    {
        var doctors = await _doctorRepository.GetByStatusAsync(
            request.Dto.Status, ct);
        var response = _mapper.Map
            <GetByStatusDoctorsResponseDto<TDoctorId, TSpecializationId>>(doctors);
        return response;
    }
}
