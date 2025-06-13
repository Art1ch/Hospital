using AutoMapper;
using DoctorAPI.Application.Contracts.UnitOfWork;
using DoctorAPI.Application.Responses.Doctor;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetById;

internal class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, GetByIdDoctorResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDoctorByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetByIdDoctorResponse> Handle(GetDoctorByIdQuery query, CancellationToken cancellationToken)
    {
        var id = query.Id;
        var result = await _unitOfWork.DoctorRepository.GetDoctorInfoById(id, cancellationToken);
        var response = _mapper.Map<GetByIdDoctorResponse>(result);
        return response;
    }
}
