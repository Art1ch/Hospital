using AutoMapper;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Responses.Doctor;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetAll;

internal class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, GetAllDoctorsResponse>
{
    private readonly IMapper _mapper;
    private readonly IDoctorRepository _doctorRepository;

    public GetAllDoctorsQueryHandler(IMapper mapper, IDoctorRepository doctorRepository)
    {
        _mapper = mapper;
        _doctorRepository = doctorRepository;
    }

    public async Task<GetAllDoctorsResponse> Handle(GetAllDoctorsQuery query, CancellationToken cancellationToken)
    {
        var page = query.Page;
        var pageSize = query.PageSize;
        var result = await _doctorRepository.GetAllDoctorsAsync(page, pageSize, cancellationToken);
        var response = _mapper.Map<GetAllDoctorsResponse>(result);
        return response;
    }
}
