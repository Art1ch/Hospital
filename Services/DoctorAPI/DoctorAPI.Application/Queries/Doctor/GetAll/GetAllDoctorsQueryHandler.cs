using AutoMapper;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.GetAll;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetAll;

public class GetAllDoctorsQueryHandler<TDoctorId, TSpecializationId>
    : IRequestHandler<GetAllDoctorsQuery<TDoctorId,
        TSpecializationId>,
        GetAllDoctorsResponseDto<TDoctorId,
            TSpecializationId>>
{
    private readonly IDoctorRepository<TDoctorId,
        TSpecializationId> _doctorRepository;
    private readonly IMapper _mapper;

    public GetAllDoctorsQueryHandler(
        IDoctorRepository<TDoctorId,
            TSpecializationId> doctorRepository,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }
    public async Task<GetAllDoctorsResponseDto<TDoctorId, TSpecializationId>> Handle(
        GetAllDoctorsQuery<TDoctorId, TSpecializationId> request,
        CancellationToken ct)
    {
        var doctors = await _doctorRepository.GetAllAsync(
            request.Page,
            request.PageSize,
            ct);
        var response = _mapper.Map<GetAllDoctorsResponseDto<TDoctorId,
            TSpecializationId>>(doctors);
        return response;
    }
}
