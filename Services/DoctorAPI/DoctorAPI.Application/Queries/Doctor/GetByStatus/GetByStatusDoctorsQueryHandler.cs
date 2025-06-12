using AutoMapper;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.Contracts.UnitOfWork;
using DoctorAPI.Application.Responses.Doctor;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetByStatus;

internal class GetByStatusDoctorsQueryHandler : IRequestHandler<GetByStatusDoctorsQuery, GetByStatusDoctorsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByStatusDoctorsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetByStatusDoctorsResponse> Handle(GetByStatusDoctorsQuery query, CancellationToken cancellationToken)
    {
        var doctorStatus = query.DoctorStatus;
        var result = await _unitOfWork.DoctorRepository.GetByStatusAsync(doctorStatus, cancellationToken);
        var response = _mapper.Map<GetByStatusDoctorsResponse>(result);
        return response;
    }
}
