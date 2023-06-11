using MassTransit;

namespace Shared.Infrastructure.MassTransit
{
    public class IntegrationEventMassTransitProducer : IIntegrationEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public IntegrationEventMassTransitProducer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public Task PublishIntegrationEventAsync<T>(T integrationEvent, CancellationToken cancellationToken) where T : class
        {
            return _publishEndpoint.Publish(integrationEvent, cancellationToken);
        }
    }
}
