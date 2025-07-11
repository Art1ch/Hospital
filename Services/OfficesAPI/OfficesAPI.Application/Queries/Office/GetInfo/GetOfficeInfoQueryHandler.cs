using AutoMapper;
using MediatR;
using OfficesAPI.Application.Contracts.Repository.Office;
using OfficesAPI.Application.Responses.Office;

namespace OfficesAPI.Application.Queries.Office.GetInfo;

internal sealed class GetOfficeInfoQueryHandler : IRequestHandler<GetOfficeInfoQuery, GetOfficeInfoResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IMapper _mapper;

    public GetOfficeInfoQueryHandler(
        IOfficeRepository officeRepository,
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
