using AutoMapper;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.GetById;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetById;

public class GetByIdDoctorQueryHandler
    : IRequestHandler<GetByIdDoctorQuery,
        GetByIdDoctorResponseDto>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public GetByIdDoctorQueryHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<GetByIdDoctorResponseDto> Handle(
        GetByIdDoctorQuery request,
        CancellationToken ct)
    {
        var doctorInfo = await _doctorRepository
            .GetByIdAsync(request.Dto.Id, ct);
        var response = _mapper.Map<GetByIdDoctorResponseDto>(doctorInfo);
        return response;
    }
}
