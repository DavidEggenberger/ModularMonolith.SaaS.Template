using Shared.Features.Domain.Exceptions;

namespace Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Domain.Exceptions
{
    public class TabsAlreadyClosedException : DomainException
    {
        public TabsAlreadyClosedException(string message) : base(message)
        {
        }
    }
}
