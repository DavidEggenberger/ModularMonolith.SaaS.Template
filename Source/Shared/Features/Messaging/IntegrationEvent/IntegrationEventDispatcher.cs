using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.Messaging.IntegrationEvent
{
    public class IntegrationEventDispatcher : IIntegrationEventDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public IntegrationEventDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task RaiseAndWaitForCompletionAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellation = default) where TIntegrationEvent : IIntegrationEvent
        {
            var eventHandlers = serviceProvider.GetServices<IIntegrationEventHandler<TIntegrationEvent>>();
            foreach (var eventHandler in eventHandlers)
            {
                await eventHandler.HandleAsync(integrationEvent, cancellation);
            }
        }
    }
}
