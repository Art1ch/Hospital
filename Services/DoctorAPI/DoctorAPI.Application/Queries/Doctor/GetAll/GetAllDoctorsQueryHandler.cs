using AutoMapper;
using DoctorAPI.Application.Contracts.UnitOfWork;
using DoctorAPI.Application.Responses.Doctor;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetAll;

internal class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, GetAllDoctorsResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDoctorsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAllDoctorsResponse> Handle(GetAllDoctorsQuery query, CancellationToken cancellationToken)
    {
        var page = query.Page;
        var pageSize = query.PageSize;
        var result = await _unitOfWork.DoctorRepository.GetAllAsync(page, pageSize, cancellationToken);
        var response = _mapper.Map<GetAllDoctorsResponse>(result);
        return response;
    }
}
