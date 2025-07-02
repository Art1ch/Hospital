using MediatR;
using OfficesAPI.Application.Abstractions.BaseRequest;
using OfficesAPI.Application.Requests.Office;

namespace OfficesAPI.Application.Commands.Office.Update;

public record UpdateOfficeCommand(
    UpdateOfficeRequest Request) : BaseRequest<UpdateOfficeRequest, Unit>(Request);
