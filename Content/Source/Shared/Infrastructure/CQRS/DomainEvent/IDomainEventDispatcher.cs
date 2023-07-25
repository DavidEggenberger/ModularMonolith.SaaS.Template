using Shared.Infrastructure.DomainKernel;

namespace Shared.Infrastructure.CQRS.DomainEvent
{
    public interface IDomainEventDispatcher
    {
        Task RaiseAsync<TDomainEvent>(TDomainEvent command, CancellationToken cancellation) where TDomainEvent : IDomainEvent;
    }
}
