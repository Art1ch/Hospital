using AutoMapper;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Responses.Doctor;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetById;

internal class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, GetByIdDoctorResponse>
{
    private readonly IMapper _mapper;
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorByIdQueryHandler(IMapper mapper, IDoctorRepository doctorRepository)
    {
        _mapper = mapper;
        _doctorRepository = doctorRepository;
    }

    public async Task<GetByIdDoctorResponse> Handle(GetDoctorByIdQuery query, CancellationToken cancellationToken)
    {
        var id = query.Id;
        var result = await _doctorRepository.GetDoctorInfoById(id, cancellationToken);
        var response = _mapper.Map<GetByIdDoctorResponse>(result);
        return response;
    }
}
