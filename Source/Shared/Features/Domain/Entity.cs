using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Kernel.DomainKernel;
using Shared.Features.Errors;
using Shared.Features.Server.ExecutionContext;
using Shared.Kernel.DomainKernel.Interfaces;

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
