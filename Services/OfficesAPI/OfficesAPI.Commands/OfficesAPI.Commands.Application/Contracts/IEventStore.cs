using OfficesAPI.Commands.Core.Entities.Base;

namespace OfficesAPI.Commands.Application.Contracts;

public interface IEventStore<TEventEntity> where TEventEntity : BaseEventEntity
{
    Task AppendAsync(TEventEntity eventEntity, CancellationToken cancellationToken = default);
}
