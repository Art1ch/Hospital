namespace OfficesAPI.Commands.Core.Entities.Base;

public abstract class BaseEventEntity()
{
    public Guid StreamId { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
