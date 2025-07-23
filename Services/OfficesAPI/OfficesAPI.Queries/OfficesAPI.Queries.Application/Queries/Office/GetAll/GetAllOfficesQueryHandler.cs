using AutoMapper;
using MediatR;
using OfficesAPI.Queries.Application.Contracts.Repository.Office;
using OfficesAPI.Queries.Application.Responses.Office;

namespace OfficesAPI.Queries.Application.Office.GetAll;

internal sealed class GetAllOfficesQueryHandler : IRequestHandler<GetAllOfficesQuery, GetAllOfficesResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IMapper _mapper;

    public GetAllOfficesQueryHandler(
        IOfficeRepository officeRepository,
        IMapper mapper)
    {
        _officeRepository = officeRepository;
        _mapper = mapper;
    }

    public async Task<GetAllOfficesResponse> Handle(GetAllOfficesQuery query, CancellationToken cancellationToken)
    {
        var request = query.Request;
        var officesResult = await _officeRepository.GetAllOfficesAsync(request.Page, request.PageSize, cancellationToken);
        var response = _mapper.Map<GetAllOfficesResponse>(officesResult);
        return response;
    }
}
