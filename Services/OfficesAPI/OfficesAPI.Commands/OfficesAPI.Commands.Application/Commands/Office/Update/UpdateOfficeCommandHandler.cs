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
    IMessagePublisher messagePublisher,
    IImageService imageService
) : IRequestHandler<UpdateOfficeCommand, Unit>
{
    public async Task<Unit> Handle(UpdateOfficeCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var entity = mapper.Map<OfficeEntity>(request);

        if (request.Image == null && !string.IsNullOrEmpty(request.ImageUrl))
        {
            entity.ImageUrl = request.ImageUrl;
        }

        if (request.Image != null && !string.IsNullOrEmpty(request.ImageUrl))
        {
            await imageService.DeleteImageAsync(request.ImageUrl);
            var imageUrl = await imageService.UploadImageAsync(request.Image);

            entity.ImageUrl = imageUrl;
        }

        if (request.Image != null && string.IsNullOrEmpty(request.ImageUrl))
        {
            var imageUrl = await imageService.UploadImageAsync(request.Image);

            entity.ImageUrl = imageUrl;
        }

        var eventEntity = mapper.Map<UpdateOfficeEntity>(entity);
        var @event = mapper.Map<OfficeUpdatedEvent>(entity);

        await eventStore.AppendAsync(eventEntity, cancellationToken);
        await messagePublisher.PublishMessageAsync(@event, cancellationToken);

        return Unit.Value;
    }
}   
