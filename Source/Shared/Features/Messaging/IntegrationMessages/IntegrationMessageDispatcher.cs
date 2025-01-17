using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.Messaging.IntegrationMessages
{
    public class IntegrationMessageDispatcher : IIntegrationMessageDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public IntegrationMessageDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task SendIntegrationEventAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellation = default) where TIntegrationEvent : IIntegrationEvent
        {
            var eventHandlers = serviceProvider.GetServices<IIntegrationEventHandler<TIntegrationEvent>>();
            foreach (var eventHandler in eventHandlers)
            {
                await eventHandler.HandleAsync(integrationEvent, cancellation);
            }
        }

        public async Task<TResponse> SendIntegrationRequestAsync<TIntegrationRequest, TResponse>(TIntegrationRequest integrationEvent, CancellationToken cancellation = default) where TIntegrationRequest : IIntegrationRequest<TResponse>
        {
            var integrationRequestHandler = serviceProvider.GetService<IIntegrationRequestHandler<TIntegrationRequest, TResponse>>();
            return await integrationRequestHandler.HandleAsync(integrationEvent, cancellation);
        }
    }
}
