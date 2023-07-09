using Shared.Infrastructure.DomainKernel.Exceptions;

namespace Modules.TenantIdentity.DomainFeatures.Domain.Exceptions
{
    public class TabsAlreadyClosedException : DomainException
    {
        public TabsAlreadyClosedException(string message) : base(message)
        {
        }
    }
}
