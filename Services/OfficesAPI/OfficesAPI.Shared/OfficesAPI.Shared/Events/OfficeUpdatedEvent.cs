using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Shared.Events;

public record OfficeUpdatedEvent(
    Guid Id,
    string Address,
    string RegistryPhoneNumber,
    OfficeStatus Status
);