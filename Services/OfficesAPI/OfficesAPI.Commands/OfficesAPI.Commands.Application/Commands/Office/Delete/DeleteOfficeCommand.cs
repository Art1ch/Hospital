using MediatR;
using OfficesAPI.Application.Abstractions.BaseRequest;
using OfficesAPI.Commands.Application.Requests;

namespace OfficesAPI.Commands.Application.Office.Delete;

public record DeleteOfficeCommand(
    DeleteOfficeRequest Request
) : BaseRequest<DeleteOfficeRequest, Unit>(Request);
