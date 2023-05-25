using MassTransit;

namespace Shared.Features.Infrastructure.MassTransit
{
    public interface IIntegrationEventConsumer<T> : IConsumer<T> where T : class
    {

    }
}
