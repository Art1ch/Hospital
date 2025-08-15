using MediatR;
using OfficesAPI.Application.Abstractions.BaseRequest;
using OfficesAPI.Commands.Application.Requests.Office;

namespace OfficesAPI.Commands.Application.Office.ChangeStatus;

public record ChangeOfficeStatusCommand(
    ChangeOfficeStatusRequest Request
) : BaseRequest<ChangeOfficeStatusRequest, Unit>(Request);
