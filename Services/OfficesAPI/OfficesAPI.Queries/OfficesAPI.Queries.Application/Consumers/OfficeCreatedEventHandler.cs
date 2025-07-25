using AutoMapper;
using MassTransit;
using OfficesAPI.Queries.Application.Contracts.Repository.Office;
using OfficesAPI.Queries.Core.Entities;
using OfficesAPI.Shared.Events;

namespace OfficesAPI.Queries.Application.Consumers;

public class OfficeCreatedEventHandler(
    IMapper mapper,
    IOfficeRepository officeRepository
) : IConsumer<OfficeCreatedEvent>
{
    public async Task Consume(ConsumeContext<OfficeCreatedEvent> context)
    {
        var entity = mapper.Map<OfficeEntity>(context.Message);
        await officeRepository.CreateAsync(entity);
    }
}
