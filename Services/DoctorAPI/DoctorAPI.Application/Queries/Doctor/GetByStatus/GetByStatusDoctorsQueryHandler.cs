using AutoMapper;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.GetByStatus;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetByStatus;

public class GetByStatusDoctorsQueryHandler<TId1, TId2>
    : IRequestHandler<GetByStatusDoctorsQuery<TId1, TId2>,
        GetByStatusDoctorsResponseDto<TId1, TId2>>
{
    private readonly IDoctorRepository<TId1, TId2> _doctorRepository;
    private readonly IMapper _mapper;

    public GetByStatusDoctorsQueryHandler(
        IDoctorRepository<TId1, TId2> doctorRepository,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<GetByStatusDoctorsResponseDto<TId1, TId2>> Handle(
        GetByStatusDoctorsQuery<TId1, TId2> request,
        CancellationToken ct)
    {
        var doctors = await _doctorRepository.GetByStatus(request.Dto.Status, ct);
        var response = _mapper.Map<GetByStatusDoctorsResponseDto<TId1, TId2>>(doctors);
        return response;
    }
}
