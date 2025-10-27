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
    IImageService imageService,
    ICommandOfficeRepository repository
) : IRequestHandler<UpdateOfficeCommand, Unit>
{
    public async Task<Unit> Handle(UpdateOfficeCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var entity = mapper.Map<OfficeEntity>(request);

        if (request.NewImage != null)
        {
            var imageUrl = await imageService.UploadImageAsync(request.NewImage);
            entity.ImageUrl = imageUrl;

            if (!string.IsNullOrEmpty(request.OldImageUrl))
            {
                await imageService.DeleteImageAsync(request.OldImageUrl);
            }
        }

        else
        {
            entity.ImageUrl = request.OldImageUrl;
        }

        var eventEntity = mapper.Map<UpdateOfficeEntity>(entity);
        var @event = mapper.Map<OfficeUpdatedEvent>(entity);

        await repository.UpdateAsync(entity, cancellationToken);
        await eventStore.AppendAsync(eventEntity, cancellationToken);
        await messagePublisher.PublishMessageAsync(@event, cancellationToken);

        return Unit.Value;
    }
}   
