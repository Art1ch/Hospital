using AutoMapper;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.GetByStatus;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetByStatus;

public class GetByStatusDoctorsQueryHandler
    : IRequestHandler<GetByStatusDoctorsQuery,
        GetByStatusDoctorsResponseDto>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public GetByStatusDoctorsQueryHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<GetByStatusDoctorsResponseDto> Handle(
        GetByStatusDoctorsQuery request,
        CancellationToken ct)
    {
        var doctorInfos = await _doctorRepository.GetByStatusAsync(
            request.Dto.Status, ct);
        var response = _mapper.Map
            <GetByStatusDoctorsResponseDto>(doctorInfos);
        return response;
    }
}
