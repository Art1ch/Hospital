using AutoMapper;
using DoctorAPI.Application.Contracts.Cache;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Responses.Doctor;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetById;

internal class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, GetByIdDoctorResponse>
{
    private readonly IMapper _mapper;
    private readonly IDoctorRepository _doctorRepository;
    private readonly ICacheService _cacheService;

    public GetDoctorByIdQueryHandler(IMapper mapper, IDoctorRepository doctorRepository, ICacheService cacheService)
    {
        _mapper = mapper;
        _doctorRepository = doctorRepository;
        _cacheService = cacheService;
    }

    public async Task<GetByIdDoctorResponse> Handle(GetDoctorByIdQuery query, CancellationToken cancellationToken)
    {
        var id = query.Id;
        var cachedResponse = await _cacheService.GetAsync<GetByIdDoctorResponse>(id.ToString());
        if (cachedResponse != null)
        {
            return cachedResponse;
        }
        var result = await _doctorRepository.GetDoctorInfoById(id, cancellationToken);
        var response = _mapper.Map<GetByIdDoctorResponse>(result);
        await _cacheService.SetAsync<GetByIdDoctorResponse>(id.ToString(), response, cancellationToken);
        return response;
    }
}
