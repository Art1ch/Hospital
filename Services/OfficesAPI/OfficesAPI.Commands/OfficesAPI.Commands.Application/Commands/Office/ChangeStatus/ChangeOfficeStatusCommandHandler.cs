using AutoMapper;
using MediatR;
using OfficesAPI.Commands.Application.Contracts;
using OfficesAPI.Commands.Core.Entities;
using OfficesAPI.Shared.Events;

namespace OfficesAPI.Application.Commands.Office.ChangeStatus;

internal sealed class ChangeOfficeStatusCommandHandler(
    IMapper mapper,
    IEventStore<ChangeOfficeStatusEntity> eventStore,
    IMessagePublisher messagePublisher
) : IRequestHandler<ChangeOfficeStatusCommand, Unit>
{
    public async Task<Unit> Handle(ChangeOfficeStatusCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var eventEntity = mapper.Map<ChangeOfficeStatusEntity>(request);
        var @event = mapper.Map<OfficeStatusChangedEvent>(request);
        await eventStore.AppendAsync(eventEntity, cancellationToken);
        await messagePublisher.PublishMessageAsync(@event, cancellationToken);
        return Unit.Value;
    }
}
