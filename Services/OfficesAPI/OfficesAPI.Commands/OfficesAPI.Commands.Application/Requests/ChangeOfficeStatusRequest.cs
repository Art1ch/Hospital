using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Commands.Application.Requests.Office;

public record ChangeOfficeStatusRequest(
    Guid Id,
    OfficeStatus Status);
