using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.Messaging.IntegrationMessages
{
    public interface IIntegrationRequestHandler<TIntegrationRequest, TResponse> where TIntegrationRequest : IIntegrationRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TIntegrationRequest integrationEvent, CancellationToken cancellation);
    }
}
