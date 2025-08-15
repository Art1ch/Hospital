using MediatR;
using OfficesAPI.Application.Abstractions.BaseRequest;
using OfficesAPI.Commands.Application.Requests.Office;

namespace OfficesAPI.Commands.Application.Office.Update;

public record UpdateOfficeCommand(
    UpdateOfficeRequest Request
) : BaseRequest<UpdateOfficeRequest, Unit>(Request);
