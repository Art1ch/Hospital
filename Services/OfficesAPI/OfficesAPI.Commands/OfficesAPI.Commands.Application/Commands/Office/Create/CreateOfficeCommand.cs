using MediatR;
using OfficesAPI.Application.Abstractions.BaseRequest;
using OfficesAPI.Commands.Application.Requests.Office;

namespace OfficesAPI.Commands.Application.Office.Create;

public record CreateOfficeCommand(
    CreateOfficeRequest Request) : BaseRequest<CreateOfficeRequest, Unit>(Request);