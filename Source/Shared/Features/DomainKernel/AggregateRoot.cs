using Shared.Kernel.BuildingBlocks.ContextAccessors;
using Shared.Kernel.Interfaces;

namespace Shared.Features.DomainKernel
{
    public class AggregateRoot : Entity, ITenantIdentifiable
    {
        public IExecutionContext ExecutionContext { get; set; }
        public virtual Guid TenantId { get; set; }
    }
}
