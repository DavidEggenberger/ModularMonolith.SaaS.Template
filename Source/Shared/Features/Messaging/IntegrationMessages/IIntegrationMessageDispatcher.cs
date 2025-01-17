using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.Messaging.IntegrationMessages
{
    public interface IIntegrationMessageDispatcher
    {
        Task SendIntegrationEventAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellation = default) where TIntegrationEvent : IIntegrationEvent;

        Task<TResponse> SendIntegrationRequestAsync<TIntegrationRequest, TResponse>(TIntegrationRequest integrationEvent, CancellationToken cancellation = default) where TIntegrationRequest : IIntegrationRequest<TResponse>;
    }
}
