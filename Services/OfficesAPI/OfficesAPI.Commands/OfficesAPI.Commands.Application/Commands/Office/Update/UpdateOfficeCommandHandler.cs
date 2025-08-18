using AutoMapper;
using MediatR;
using OfficesAPI.Commands.Application.Contracts;
using OfficesAPI.Commands.Core.Entities;
using OfficesAPI.Shared.Entities;
using OfficesAPI.Shared.Events;

namespace OfficesAPI.Commands.Application.Office.Update;

internal sealed class UpdateOfficeCommandHandler(
    IMapper mapper,
    IEventStore<UpdateOfficeEntity> eventStore,
    IMessagePublisher messagePublisher
) : IRequestHandler<UpdateOfficeCommand, Unit>
{
    public async Task<Unit> Handle(UpdateOfficeCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var entity = mapper.Map<OfficeEntity>(request);

        var eventEntity = mapper.Map<UpdateOfficeEntity>(entity);
        var @event = mapper.Map<OfficeUpdatedEvent>(entity);

        await eventStore.AppendAsync(eventEntity, cancellationToken);
        await messagePublisher.PublishMessageAsync(eventEntity, cancellationToken);

        return Unit.Value;
    }
}   
