using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Shared.Events;

public sealed record OfficeStatusChangedEvent(
   Guid Id,
   OfficeStatus Status
);