using OfficesAPI.Queries.Application.Abstractions.BaseRequest;
using OfficesAPI.Queries.Application.Responses.Office;

namespace OfficesAPI.Queries.Application.Queries.Office.GetInfo;

public record GetOfficeInfoQuery(
    Guid Id
) : BaseRequest<Guid, GetOfficeInfoResponse>(Id);
