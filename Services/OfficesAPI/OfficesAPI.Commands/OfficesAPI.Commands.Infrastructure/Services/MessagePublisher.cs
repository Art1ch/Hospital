using MassTransit;
using OfficesAPI.Commands.Application.Contracts;

namespace OfficesAPI.Commands.Infrastructure.Services;

internal class MessagePublisher(
    IPublishEndpoint publishEndpoint     
) : IMessagePublisher
{
    public async Task PublishMessageAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
    {
        await publishEndpoint.Publish(message, cancellationToken);
    }
}
