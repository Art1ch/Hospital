using MediatR;
using OfficesAPI.Application.Abstractions.BaseRequest;
using OfficesAPI.Application.Requests.Office;

namespace OfficesAPI.Application.Commands.Office.Create;

public record CreateOfficeCommand(
    CreateOfficeRequest Request) : BaseRequest<CreateOfficeRequest, Unit>(Request);