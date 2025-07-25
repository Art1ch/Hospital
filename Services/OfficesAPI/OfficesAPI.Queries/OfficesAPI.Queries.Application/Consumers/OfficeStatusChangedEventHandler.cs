using MassTransit;
using OfficesAPI.Queries.Application.Contracts.Repository.Office;
using OfficesAPI.Shared.Events;

namespace OfficesAPI.Queries.Application.Consumers;

public class OfficeStatusChangedEventHandler(
    IOfficeRepository officeRepository    
) : IConsumer<OfficeStatusChangedEvent>
{
    public async Task Consume(ConsumeContext<OfficeStatusChangedEvent> context)
    {
        var id = context.Message.Id;
        var status = context.Message.Status;
        await officeRepository.ChangeOfficeStatusAsync(id, status);
    }
}
