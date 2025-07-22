using MediatR;
using OfficesAPI.Commands.Application.Commands.Office.Delete;
using OfficesAPI.Commands.Application.Contracts;
using OfficesAPI.Commands.Core.Entities;
using OfficesAPI.Shared.Events;

namespace OfficesAPI.Application.Commands.Office.Delete;

internal sealed class DeleteOfficeCommandHandler(
    IEventStore<DeleteOfficeEntity> eventStore,
    IMessagePublisher messagePublisher
) : IRequestHandler<DeleteOfficeCommand, Unit>
{
    public async Task<Unit> Handle(DeleteOfficeCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var eventEntity = new DeleteOfficeEntity { Id = id };
        var @event = new OfficeDeletedEvent { Id = id };
        await eventStore.AppendAsync(eventEntity, cancellationToken);
        await messagePublisher.PublishMessageAsync(@event, cancellationToken);
        return Unit.Value;
    }
}