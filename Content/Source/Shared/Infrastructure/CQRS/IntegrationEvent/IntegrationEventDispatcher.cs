using Microsoft.Extensions.DependencyInjection;
using Shared.DomainFeatures.DomainKernel.Events;

namespace Shared.Infrastructure.CQRS.IntegrationEvent
{
    public class IntegrationEventDispatcher : IIntegrationEventDispatcher
    {
        private readonly IServiceProvider serviceProvider;
        public IntegrationEventDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public void Raise<TDomainEvent>(TDomainEvent command, CancellationToken cancellation = default) where TDomainEvent : IDomainEvent
        {
            var eventHandlers = serviceProvider.GetServices<IIntegrationEventHandler<TDomainEvent>>();
            foreach (var eventHandler in eventHandlers)
            {
                eventHandler.HandleAsync(command, cancellation);
            }
        }
    }
}
