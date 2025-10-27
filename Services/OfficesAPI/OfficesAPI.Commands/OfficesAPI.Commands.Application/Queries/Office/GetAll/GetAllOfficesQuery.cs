using OfficesAPI.Application.Abstractions.BaseRequest;
using OfficesAPI.Shared.Requests;
using OfficesAPI.Shared.Responses;

namespace OfficesAPI.Commands.Application.Queries.Office.GetAll;

public record GetAllOfficesQuery(
    GetAllOfficesRequest Request
) : BaseRequest<GetAllOfficesRequest, GetAllOfficesResponse>(Request);