namespace FinancialControl.Application.Interfaces;

using System.Threading;
using System.Threading.Tasks;
public interface IEventPublisher
{
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default);
}