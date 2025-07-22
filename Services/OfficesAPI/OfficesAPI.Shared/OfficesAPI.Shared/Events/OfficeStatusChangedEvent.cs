using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Shared.Events;

public class OfficeStatusChangedEvent
{
    public Guid Id { get; set; }
    public OfficeStatus Status { get; set; }
}
