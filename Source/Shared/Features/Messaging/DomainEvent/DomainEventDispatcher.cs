using Microsoft.Extensions.DependencyInjection;

namespace Shared.Features.Messaging.DomainEvent
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider serviceProvider;
        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task PublishAsync<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellation = default) where TDomainEvent : IDomainEvent
        {
            var eventHandlers = serviceProvider.GetServices<IDomainEventHandler<TDomainEvent>>();
            foreach (var eventHandler in eventHandlers)
            {
                await eventHandler.HandleAsync(domainEvent, cancellation);
            }
        }
    }
}
