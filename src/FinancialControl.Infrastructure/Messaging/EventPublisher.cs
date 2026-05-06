using FinancialControl.Application.Interfaces;
using MassTransit;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialControl.Infrastructure.Messaging;

public class EventPublisher : IEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishAsync<T>(
     T @event,
     CancellationToken cancellationToken = default)
    {
        if (@event is null)
            throw new ArgumentNullException(nameof(@event));

        await _publishEndpoint.Publish(@event, cancellationToken);
    }
}