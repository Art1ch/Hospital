using AutoMapper;
using MediatR;
using OfficesAPI.Commands.Application.Contracts;
using OfficesAPI.Commands.Core.Entities;
using OfficesAPI.Shared.Events;

namespace OfficesAPI.Commands.Application.Office.Create;

internal sealed class CreateOfficeCommandHandler(
    IMapper mapper,
    IEventStore<CreateOfficeEntity> eventStore,
    IMessagePublisher messagePublisher
) : IRequestHandler<CreateOfficeCommand, Unit>
{
    public async Task<Unit> Handle(CreateOfficeCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var eventEntity = mapper.Map<CreateOfficeEntity>(request);
        var @event = mapper.Map<OfficeCreatedEvent>(request);
        @event.Id = eventEntity.Id;
        await eventStore.AppendAsync(eventEntity, cancellationToken);
        await messagePublisher.PublishMessageAsync(@event);
        return Unit.Value;
    }
}
