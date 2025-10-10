using OfficesAPI.Queries.Application.Abstractions.BaseRequest;
using OfficesAPI.Shared.Requests;
using OfficesAPI.Shared.Responses;

namespace OfficesAPI.Queries.Application.Office.GetAll;

public record GetAllOfficesQuery(
    GetAllOfficesRequest Request
) : BaseRequest<GetAllOfficesRequest, GetAllOfficesResponse>(Request);
