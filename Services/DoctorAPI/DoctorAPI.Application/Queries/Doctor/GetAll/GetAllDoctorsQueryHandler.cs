using AutoMapper;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.GetAll;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetAll;

public class GetAllDoctorsQueryHandler
    : IRequestHandler<GetAllDoctorsQuery,
        GetAllDoctorsResponseDto>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public GetAllDoctorsQueryHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }
    public async Task<GetAllDoctorsResponseDto> Handle(
        GetAllDoctorsQuery request,
        CancellationToken ct)
    {
        var doctorInfos = await _doctorRepository.GetAllAsync(
            request.Dto.Page,
            request.Dto.PageSize,
            ct);
        var response = _mapper.Map<GetAllDoctorsResponseDto>(
            doctorInfos);
        return response;
    }
}
