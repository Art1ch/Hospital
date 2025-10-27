using OfficesAPI.Queries.Application.Abstractions.BaseRequest;
using OfficesAPI.Shared.Responses;

namespace OfficesAPI.Queries.Application.Queries.Office.GetInfo;

public record GetOfficeInfoQuery(
    Guid Id
) : BaseRequest<Guid, GetOfficeInfoResponse>(Id);
