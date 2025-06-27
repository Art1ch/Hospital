using AutoMapper;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Responses.Doctor;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetBySpecialization;

internal class GetDoctorBySpecializationQueryHandler : IRequestHandler<GetDoctorBySpecializationQuery, GetBySpecializationDoctorResponse>
{
    private readonly IMapper _mapper;
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorBySpecializationQueryHandler(IMapper mapper, IDoctorRepository doctorRepository)
    {
        _mapper = mapper;
        _doctorRepository = doctorRepository;
    }

    public async Task<GetBySpecializationDoctorResponse> Handle(GetDoctorBySpecializationQuery query, CancellationToken cancellationToken)
    {
        var specializationId = query.SpecializationId;
        var result = await _doctorRepository.GetDoctorBySpecializationAsync(specializationId, cancellationToken);
        var response = _mapper.Map<GetBySpecializationDoctorResponse>(result);
        return response;
    }
}
