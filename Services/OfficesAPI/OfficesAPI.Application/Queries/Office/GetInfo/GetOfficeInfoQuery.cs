using OfficesAPI.Application.Abstractions.BaseRequest;
using OfficesAPI.Application.Responses.Office;

namespace OfficesAPI.Application.Queries.Office.GetInfo;

public record GetOfficeInfoQuery(
    Guid Id) : BaseRequest<Guid, GetOfficeInfoResponse>(Id);
