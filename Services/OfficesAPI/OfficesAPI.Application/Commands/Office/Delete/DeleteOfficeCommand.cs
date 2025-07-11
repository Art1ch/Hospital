using MediatR;
using OfficesAPI.Application.Abstractions.BaseRequest;

namespace OfficesAPI.Application.Commands.Office.Delete;

public record DeleteOfficeCommand(
    Guid Id) : BaseRequest<Guid, Unit>(Id);
