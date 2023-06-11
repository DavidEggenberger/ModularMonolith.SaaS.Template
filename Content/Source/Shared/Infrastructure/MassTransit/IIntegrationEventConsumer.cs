using MassTransit;

namespace Shared.Infrastructure.MassTransit
{
    public interface IIntegrationEventConsumer<T> : IConsumer<T> where T : class
    {

    }
}
