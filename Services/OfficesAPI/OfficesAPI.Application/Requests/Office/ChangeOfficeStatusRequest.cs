using OfficesAPI.Core.Enums;

namespace OfficesAPI.Application.Requests.Office;

public record ChangeOfficeStatusRequest(
    Guid Id,
    OfficeStatus Status);
