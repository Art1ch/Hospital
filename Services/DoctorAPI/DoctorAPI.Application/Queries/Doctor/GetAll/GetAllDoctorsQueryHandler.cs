using AutoMapper;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.GetAll;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetAll;

public class GetAllDoctorsQueryHandler<TId1, TId2>
    : IRequestHandler<GetAllDoctorsQuery<TId1, TId2>, GetAllDoctorsResponseDto<TId1, TId2>>
{
    private readonly IDoctorRepository<TId1, TId2> _doctorRepository;
    private readonly IMapper _mapper;

    public GetAllDoctorsQueryHandler(
        IDoctorRepository<TId1, TId2> doctorRepository,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }
    public async Task<GetAllDoctorsResponseDto<TId1, TId2>> Handle(
        GetAllDoctorsQuery<TId1, TId2> request,
        CancellationToken ct)
    {
        var doctors = await _doctorRepository.GetAll(
            request.Page,
            request.PageSize,
            ct);
        var response = _mapper.Map<GetAllDoctorsResponseDto<TId1, TId2>>(doctors);
        return response;
    }
}
