using OfficesAPI.Application.Abstractions.BaseRequest;
using OfficesAPI.Shared.Responses;

namespace OfficesAPI.Commands.Application.Queries.Office.GetInfo;

public record GetOfficeInfoQuery(
    Guid Id   
) : BaseRequest<Guid, GetOfficeInfoResponse>(Id);