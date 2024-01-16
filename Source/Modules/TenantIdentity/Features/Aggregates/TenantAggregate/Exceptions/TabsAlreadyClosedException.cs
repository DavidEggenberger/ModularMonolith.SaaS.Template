using Shared.Features.DomainKernel.Exceptions;

namespace Modules.TenantIdentity.Features.Domain.TenantAggregate.Exceptions
{
    public class TabsAlreadyClosedException : DomainException
    {
        public TabsAlreadyClosedException(string message) : base(message)
        {
        }
    }
}
