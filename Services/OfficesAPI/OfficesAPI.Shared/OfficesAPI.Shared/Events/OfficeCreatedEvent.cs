using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Shared.Events;

public class OfficeCreatedEvent
{
    public Guid Id { get; set; }
    public string Address { get; set; }
    public string RegistryPhoneNumber { get; set; }
    public OfficeStatus Status { get; set; }
}
