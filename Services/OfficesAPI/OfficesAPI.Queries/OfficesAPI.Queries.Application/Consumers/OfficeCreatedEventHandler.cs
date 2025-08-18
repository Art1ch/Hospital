using AutoMapper;
using MassTransit;
using OfficesAPI.Queries.Application.Contracts.Repository.Office;
using OfficesAPI.Shared.Events;

namespace OfficesAPI.Queries.Application.Consumers;

public class OfficeCreatedEventHandler(
    IMapper mapper,
    IOfficeRepository officeRepository
) : IConsumer<OfficeCreatedEvent>
{
    public async Task Consume(ConsumeContext<OfficeCreatedEvent> context)
    {
        var entity = context.Message.Entity;
        await officeRepository.CreateAsync(entity);
    }
}
