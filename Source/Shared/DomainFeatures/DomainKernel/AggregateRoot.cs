using Shared.Kernel.Interfaces;

namespace Shared.Features.DomainKernel
{
    public class AggregateRoot : Entity, ITenantIdentifiable
    {
        public virtual Guid TenantId { get; set; }
    }
}
