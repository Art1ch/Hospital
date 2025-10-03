using AutoMapper;
using MediatR;
using OfficesAPI.Commands.Application.Contracts;
using OfficesAPI.Commands.Core.Entities;
using OfficesAPI.Shared.Events;

namespace OfficesAPI.Commands.Application.Office.Delete;

internal sealed class DeleteOfficeCommandHandler(
    IEventStore<DeleteOfficeEntity> eventStore,
    IMapper mapper,
    IMessagePublisher messagePublisher,
    IImageService imageService
) : IRequestHandler<DeleteOfficeCommand, Unit>
{
    public async Task<Unit> Handle(DeleteOfficeCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        if (!string.IsNullOrEmpty(request.ImageUrl))
        {
            await imageService.DeleteImageAsync(request.ImageUrl);
        }

        var eventEntity = mapper.Map<DeleteOfficeEntity>(request);
        var @event = mapper.Map<OfficeDeletedEvent>(request);

        await eventStore.AppendAsync(eventEntity, cancellationToken);
        await messagePublisher.PublishMessageAsync(@event, cancellationToken);

        return Unit.Value;
    }
}