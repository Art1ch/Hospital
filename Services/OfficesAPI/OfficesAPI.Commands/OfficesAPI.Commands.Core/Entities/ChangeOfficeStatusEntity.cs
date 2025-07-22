using OfficesAPI.Commands.Core.Entities.Base;
using OfficesAPI.Commands.Core.Enums;
using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Commands.Core.Entities;

public class ChangeOfficeStatusEntity : BaseEventEntity
{
    public Guid Id { get; set; }
    public OfficeStatus Status { get; set; }
}
