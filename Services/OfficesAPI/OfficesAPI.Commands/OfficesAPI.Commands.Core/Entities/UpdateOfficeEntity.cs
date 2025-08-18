using OfficesAPI.Commands.Core.Entities.Base;
using OfficesAPI.Shared.Entities;

namespace OfficesAPI.Commands.Core.Entities;

public class UpdateOfficeEntity : BaseEventEntity
{
    public OfficeEntity Entity { get; set; }   
}
