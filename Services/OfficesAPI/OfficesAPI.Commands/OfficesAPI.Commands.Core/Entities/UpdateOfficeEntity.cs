using OfficesAPI.Commands.Core.Entities.Base;
using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Commands.Core.Entities;

public class UpdateOfficeEntity : BaseEventEntity
{
    public Guid Id { get; set; }
    public string Address { get; set; }
    public string RegistryPhoneNumber { get; set; }
    public OfficeStatus Status { get; set; }
}
