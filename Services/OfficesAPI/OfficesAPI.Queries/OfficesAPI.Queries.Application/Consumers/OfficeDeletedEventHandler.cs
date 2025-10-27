using MassTransit;
using OfficesAPI.Queries.Application.Contracts.Repository.Office;
using OfficesAPI.Shared.Events;

namespace OfficesAPI.Queries.Application.Consumers;

public class OfficeDeletedEventHandler(
    IQueryOfficeRepository officeRepository    
) : IConsumer<OfficeDeletedEvent>
{
    public async Task Consume(ConsumeContext<OfficeDeletedEvent> context)
    {
        await officeRepository.DeleteAsync(context.Message.Id);
    }
}
