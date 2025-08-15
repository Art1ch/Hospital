using AutoMapper;
using MediatR;
using OfficesAPI.Commands.Application.Contracts;
using OfficesAPI.Commands.Core.Entities;
using OfficesAPI.Shared.Entities;
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
        var entity = mapper.Map<OfficeEntity>(request);

        var eventEntity = mapper.Map<CreateOfficeEntity>(entity);
        var @event = new OfficeCreatedEvent(entity);

        await eventStore.AppendAsync(eventEntity, cancellationToken);
        await messagePublisher.PublishMessageAsync(@event);

        return Unit.Value;
    }
}
