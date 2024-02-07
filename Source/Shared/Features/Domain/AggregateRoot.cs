using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.Auth;
using Shared.Kernel.BuildingBlocks.Auth.Exceptions;
using Shared.Kernel.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Features.Domain
{
    public class AggregateRoot : Entity, ITenantIdentifiable
    {
        [NotMapped]
        public IExecutionContext ExecutionContext { get; set; }
        public virtual Guid TenantId { get; set; }

        public void ThrowIfCallerIsNotInRole(TenantRole role)
        {
            if (ExecutionContext.TenantRole != role)
            {
                throw new UnauthorizedException();
            }
        }
    }
}
