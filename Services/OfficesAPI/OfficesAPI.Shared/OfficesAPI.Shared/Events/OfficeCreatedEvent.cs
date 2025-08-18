using OfficesAPI.Shared.Entities;

namespace OfficesAPI.Shared.Events;

public sealed record OfficeCreatedEvent(
    OfficeEntity Entity
);