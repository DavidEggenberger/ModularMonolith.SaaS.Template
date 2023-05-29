using MassTransit;

namespace Shared.DomainFeatures.Infrastructure.MassTransit
{
    public interface IIntegrationEventConsumer<T> : IConsumer<T> where T : class
    {

    }
}
