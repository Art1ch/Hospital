using OfficesAPI.Application.Abstractions.BaseRequest;
using OfficesAPI.Application.Requests.Office;
using OfficesAPI.Application.Responses.Office;

namespace OfficesAPI.Application.Queries.Office.GetAll;

public record GetAllOfficesQuery(
    GetAllOfficesRequest Request) : BaseRequest<GetAllOfficesRequest, GetAllOfficesResponse>(Request);
