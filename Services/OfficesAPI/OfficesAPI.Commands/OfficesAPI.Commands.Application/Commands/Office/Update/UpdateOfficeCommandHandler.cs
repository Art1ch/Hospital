using AutoMapper;
using MediatR;
using OfficesAPI.Commands.Application.Contracts;
using OfficesAPI.Commands.Core.Entities;
using OfficesAPI.Shared.Events;

namespace OfficesAPI.Application.Commands.Office.Update;

internal sealed class UpdateOfficeCommandHandler(
    IMapper mapper,
    IEventStore<UpdateOfficeEntity> eventStore,
    IMessagePublisher messagePublisher
) : IRequestHandler<UpdateOfficeCommand, Unit>
{
    public async Task<Unit> Handle(UpdateOfficeCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var eventEntity = mapper.Map<UpdateOfficeEntity>(request);
        var @event = mapper.Map<OfficeUpdatedEvent>(request);
        await eventStore.AppendAsync(eventEntity, cancellationToken);
        await messagePublisher.PublishMessageAsync(eventEntity, cancellationToken);
        return Unit.Value;
    }
}   
