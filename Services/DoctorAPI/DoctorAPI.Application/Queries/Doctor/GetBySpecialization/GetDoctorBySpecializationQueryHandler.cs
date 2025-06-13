using AutoMapper;
using DoctorAPI.Application.Contracts.UnitOfWork;
using DoctorAPI.Application.Responses.Doctor;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetBySpecialization;

internal class GetDoctorBySpecializationQueryHandler : IRequestHandler<GetDoctorBySpecializationQuery, GetBySpecializationDoctorResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDoctorBySpecializationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetBySpecializationDoctorResponse> Handle(GetDoctorBySpecializationQuery query, CancellationToken cancellationToken)
    {
        var specializationId = query.SpecializationId;
        var result = await _unitOfWork.DoctorRepository.GetDoctorBySpecializationAsync(specializationId, cancellationToken);
        var response = _mapper.Map<GetBySpecializationDoctorResponse>(result);
        return response;
    }
}
