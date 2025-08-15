using AutoMapper;
using MassTransit;
using OfficesAPI.Queries.Application.Contracts.Repository.Office;
using OfficesAPI.Shared.Events;

namespace OfficesAPI.Queries.Application.Consumers;

public class OfficeUpdatedEventHandler(
    IMapper mapper,
    IOfficeRepository officeRepository    
) : IConsumer<OfficeUpdatedEvent>
{
    public async Task Consume(ConsumeContext<OfficeUpdatedEvent> context)
    {
        var entity = context.Message.Entity;
        await officeRepository.UpdateAsync(entity);
    }
}
