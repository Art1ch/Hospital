using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Shared.Events;

public record OfficeStatusChangedEvent(
   Guid Id,
   OfficeStatus Status
);