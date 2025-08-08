using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Shared.Events;

public record OfficeCreatedEvent(
    Guid Id,
    string Address,
    string RegistryPhoneNumber,
    OfficeStatus Status
);