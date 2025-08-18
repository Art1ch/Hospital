using OfficesAPI.Queries.Application.Abstractions.BaseRequest;
using OfficesAPI.Queries.Application.Requests.Office;
using OfficesAPI.Queries.Application.Responses.Office;

namespace OfficesAPI.Queries.Application.Office.GetAll;

public record GetAllOfficesQuery(
    GetAllOfficesRequest Request
) : BaseRequest<GetAllOfficesRequest, GetAllOfficesResponse>(Request);
