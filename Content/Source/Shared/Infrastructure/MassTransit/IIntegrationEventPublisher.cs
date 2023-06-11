namespace Shared.Infrastructure.MassTransit
{
    public interface IIntegrationEventPublisher
    {
        Task PublishIntegrationEventAsync<T>(T integrationEvent, CancellationToken cancellationToken) where T : class;
    }
}
