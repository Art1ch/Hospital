using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Queries.Core.Entities;

public class OfficeEntity
{
    public Guid Id { get; set; }
    public string Address { get; set; }
    public string RegistryPhoneNumber { get; set; }
    public OfficeStatus Status { get; set; }
}
