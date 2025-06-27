using AutoMapper;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Responses.Doctor;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetByStatus;

internal class GetDoctorsByStatusQueryHandler : IRequestHandler<GetDoctorsByStatusQuery, GetByStatusDoctorsResponse>
{
    private readonly IMapper _mapper;
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorsByStatusQueryHandler(IDoctorRepository doctorRepository, IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<GetByStatusDoctorsResponse> Handle(GetDoctorsByStatusQuery query, CancellationToken cancellationToken)
    {
        var doctorStatus = query.DoctorStatus;
        var result = await _doctorRepository.GetDoctorByStatusAsync(doctorStatus, cancellationToken);
        var response = _mapper.Map<GetByStatusDoctorsResponse>(result);
        return response;
    }
}
