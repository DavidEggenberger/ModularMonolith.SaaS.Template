using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Kernel.BuildingBlocks.Auth.Exceptions;
using Shared.Kernel.DomainKernel;
using Shared.Kernel.BuildingBlocks.Auth;
using Shared.Features.Errors;

namespace Shared.Features.Domain
{
    public abstract class Entity : IAuditable, IIdentifiable, IConcurrent
    {
        [Key]
        public Guid Id { get; set; }

        public virtual Guid TenantId { get; set; }

        [ConcurrencyCheck]
        public byte[] RowVersion { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset LastUpdatedAt { get; set; }

        [NotMapped]
        public IExecutionContext ExecutionContext { get; set; }

        public void ThrowIfCallerIsNotInRole(TenantRole role)
        {
            if (ExecutionContext.TenantRole != role)
            {
                throw Error.UnAuthorized;
            }
        }
    }
}
