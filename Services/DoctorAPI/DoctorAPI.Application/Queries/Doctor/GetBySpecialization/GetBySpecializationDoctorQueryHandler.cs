using AutoMapper;
using DoctorAPI.Application.Contracts.UnitOfWork;
using DoctorAPI.Application.Responses.Doctor;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetBySpecialization;

internal class GetBySpecializationDoctorQueryHandler : IRequestHandler<GetBySpecializationDoctorQuery, GetBySpecializationDoctorResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBySpecializationDoctorQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetBySpecializationDoctorResponse> Handle(GetBySpecializationDoctorQuery query, CancellationToken cancellationToken)
    {
        var specializationId = query.SpecializationId;
        var result = await _unitOfWork.DoctorRepository.GetBySpecializationAsync(specializationId, cancellationToken);
        var response = _mapper.Map<GetBySpecializationDoctorResponse>(result);
        return response;
    }
}
