using AutoMapper;
using MediatR;
using OfficesAPI.Queries.Application.Contracts.Repository.Office;
using OfficesAPI.Shared.Responses;

namespace OfficesAPI.Queries.Application.Office.GetAll;

internal sealed class GetAllOfficesQueryHandler : IRequestHandler<GetAllOfficesQuery, GetAllOfficesResponse>
{
    private readonly IQueryOfficeRepository _officeRepository;
    private readonly IMapper _mapper;

    public GetAllOfficesQueryHandler(
        IQueryOfficeRepository officeRepository,
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
