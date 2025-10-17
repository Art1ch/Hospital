using AutoMapper;
using MediatR;
using OfficesAPI.Commands.Application.Contracts;
using OfficesAPI.Shared.Responses;

namespace OfficesAPI.Commands.Application.Queries.Office.GetAll;

internal sealed class GetAllOfficesQueryHandler : IRequestHandler<GetAllOfficesQuery, GetAllOfficesResponse>
{
    private readonly ICommandOfficeRepository _officeRepository;
    private readonly IMapper _mapper;

    public GetAllOfficesQueryHandler(
        ICommandOfficeRepository officeRepository,
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

