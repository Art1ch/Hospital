using MediatR;
using OfficesAPI.Application.Abstractions.BaseRequest;

namespace OfficesAPI.Commands.Application.Office.Delete;

public record DeleteOfficeCommand(
    Guid Id) : BaseRequest<Guid, Unit>(Id);
