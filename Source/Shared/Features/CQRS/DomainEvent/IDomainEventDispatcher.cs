using Shared.Features.DomainKernel;

namespace Shared.Features.CQRS.DomainEvent
{
    public interface IDomainEventDispatcher
    {
        Task RaiseAsync<TDomainEvent>(TDomainEvent command, CancellationToken cancellation) where TDomainEvent : IDomainEvent;
    }
}
