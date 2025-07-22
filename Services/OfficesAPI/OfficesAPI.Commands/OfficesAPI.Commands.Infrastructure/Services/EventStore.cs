using EventStore.Client;
using OfficesAPI.Commands.Application.Contracts;
using OfficesAPI.Commands.Core.Entities.Base;
using static Grpc.Core.Metadata;
using System.Text.Json;

namespace OfficesAPI.Commands.Infrastructure.Services;

internal class EventStore<TEventEntity>(
    EventStoreClient eventStoreClient
) : IEventStore<TEventEntity> where TEventEntity : BaseEventEntity
{
    public async Task AppendAsync(TEventEntity eventEntity, CancellationToken cancellationToken = default)
    {
        var streamName = eventEntity.StreamId.ToString();

        var eventPayload = JsonSerializer.SerializeToUtf8Bytes(eventEntity, eventEntity.GetType());

        var eventType = eventEntity.GetType().Name;

        var eventData = new EventData(
            Uuid.NewUuid(),
            eventType,
            eventPayload
        );

        await eventStoreClient.AppendToStreamAsync(
            streamName,
            StreamState.Any,
            new[] { eventData },
            cancellationToken : cancellationToken
        );
    }
}
