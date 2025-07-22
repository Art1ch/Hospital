using OfficesAPI.Commands.Core.Entities.Base;

namespace OfficesAPI.Commands.Core.Entities;

public class DeleteOfficeEntity : BaseEventEntity
{
    public Guid Id { get; set; }
}
