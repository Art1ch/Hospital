using MassTransit;
using OfficesAPI.Queries.Application.Contracts.Repository.Office;
using OfficesAPI.Shared.Events;

namespace OfficesAPI.Queries.Application.Consumers;

public class OfficeDeletedEventHandler(
    IOfficeRepository officeRepository    
) : IConsumer<OfficeDeletedEvent>
{
    public async Task Consume(ConsumeContext<OfficeDeletedEvent> context)
    {
        await officeRepository.DeleteAsync(context.Message.Id);
    }
}
