using Shared.Infrastructure.DomainKernel.Exceptions;

namespace Modules.TenantIdentity.DomainFeatures.Aggregates.TenantAggregate.Domain.Exceptions
{
    public class TabsAlreadyClosedException : DomainException
    {
        public TabsAlreadyClosedException(string message) : base(message)
        {
        }
    }
}
