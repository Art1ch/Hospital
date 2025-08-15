using OfficesAPI.Shared.Entities;

namespace OfficesAPI.Shared.Events;

public sealed record OfficeUpdatedEvent(
    OfficeEntity Entity
);