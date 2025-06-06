using AutoMapper;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.GetById;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetById;

public class GetByIdDoctorQueryHandler<TId1, TId2>
    : IRequestHandler<GetByIdDoctorQuery<TId1, TId2>, GetByIdDoctorResponseDto<TId1, TId2>>
{
    private readonly IDoctorRepository<TId1, TId2> _doctorRepository;
    private readonly IMapper _mapper;
    public GetByIdDoctorQueryHandler(
        IDoctorRepository<TId1, TId2> doctorRepository,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }
    public async Task<GetByIdDoctorResponseDto<TId1, TId2>> Handle(
        GetByIdDoctorQuery<TId1, TId2> request,
        CancellationToken ct)
    {
        var doctor = await _doctorRepository.GetById(request.Dto.Id, ct);
        var response = _mapper.Map<GetByIdDoctorResponseDto<TId1, TId2>>(doctor);
        return response;
    }
}
