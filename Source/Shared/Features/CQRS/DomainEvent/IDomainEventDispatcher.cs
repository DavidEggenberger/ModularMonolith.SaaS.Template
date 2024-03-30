namespace Shared.Features.CQRS.DomainEvent
{
    public interface IDomainEventDispatcher
    {
        Task PublishAsync<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellation) where TDomainEvent : IDomainEvent;
    }
}
