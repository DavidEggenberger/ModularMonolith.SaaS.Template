using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Interfaces;

namespace Shared.Infrastructure.CQRS.DomainEvent
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider serviceProvider;
        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task RaiseAsync<TDomainEvent>(TDomainEvent command, CancellationToken cancellation = default) where TDomainEvent : IDomainEvent
        {
            var eventHandlers = serviceProvider.GetServices<IDomainEventHandler<TDomainEvent>>();
            foreach (var eventHandler in eventHandlers)
            {
                await eventHandler.HandleAsync(command, cancellation);
            }
        }
    }
}
