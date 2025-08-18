using AutoMapper;
using DoctorAPI.Application.Contracts.Cache;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Responses.Doctor;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetBySpecialization;

internal class GetDoctorBySpecializationQueryHandler : IRequestHandler<GetDoctorBySpecializationQuery, GetBySpecializationDoctorResponse>
{
    private readonly IMapper _mapper;
    private readonly IDoctorRepository _doctorRepository;
    private readonly ICacheService _cacheService;

    public GetDoctorBySpecializationQueryHandler(IMapper mapper, IDoctorRepository doctorRepository, ICacheService cacheService)
    {
        _mapper = mapper;
        _doctorRepository = doctorRepository;
        _cacheService = cacheService;
    }

    public async Task<GetBySpecializationDoctorResponse> Handle(GetDoctorBySpecializationQuery query, CancellationToken cancellationToken)
    {
        var specializationId = query.SpecializationId;
        var cachedResponse = await _cacheService.GetAsync<GetBySpecializationDoctorResponse>(specializationId.ToString(), cancellationToken);
        if (cachedResponse != null)
        {
            return cachedResponse;
        }
        var result = await _doctorRepository.GetDoctorBySpecializationAsync(specializationId, cancellationToken);
        var response = _mapper.Map<GetBySpecializationDoctorResponse>(result);
        await _cacheService.SetAsync<GetBySpecializationDoctorResponse>(specializationId.ToString(), response, cancellationToken);
        return response;
    }
}
