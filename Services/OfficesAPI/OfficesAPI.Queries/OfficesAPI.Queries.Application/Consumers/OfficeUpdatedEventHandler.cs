using AutoMapper;
using MassTransit;
using OfficesAPI.Queries.Application.Contracts.Repository.Office;
using OfficesAPI.Queries.Core.Entities;
using OfficesAPI.Shared.Events;

namespace OfficesAPI.Queries.Application.Consumers;

internal class OfficeUpdatedEventHandler(
    IMapper mapper,
    IOfficeRepository officeRepository    
) : IConsumer<OfficeUpdatedEvent>
{
    public async Task Consume(ConsumeContext<OfficeUpdatedEvent> context)
    {
        var entity = mapper.Map<OfficeEntity>(context.Message);
        await officeRepository.UpdateAsync(entity);
    }
}
