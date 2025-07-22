using MediatR;
using OfficesAPI.Application.Abstractions.BaseRequest;

namespace OfficesAPI.Commands.Application.Commands.Office.Delete;

public record DeleteOfficeCommand(
    Guid Id) : BaseRequest<Guid, Unit>(Id);
