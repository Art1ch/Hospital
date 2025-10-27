using AutoMapper;
using MediatR;
using OfficesAPI.Commands.Application.Contracts;
using OfficesAPI.Shared.Responses;

namespace OfficesAPI.Commands.Application.Queries.Office.GetInfo;

internal sealed class GetOfficeInfoQueryHandler : IRequestHandler<GetOfficeInfoQuery, GetOfficeInfoResponse>
{
    private readonly ICommandOfficeRepository _officeRepository;
    private readonly IMapper _mapper;

    public GetOfficeInfoQueryHandler(
        ICommandOfficeRepository officeRepository,
        IMapper mapper)
    {
        _officeRepository = officeRepository;
        _mapper = mapper;
    }

    public async Task<GetOfficeInfoResponse> Handle(GetOfficeInfoQuery query, CancellationToken cancellationToken)
    {
        var officeInfo = await _officeRepository.GetOfficeInfoAsync(query.Id, cancellationToken);
        var response = _mapper.Map<GetOfficeInfoResponse>(officeInfo);
        return response;
    }
}