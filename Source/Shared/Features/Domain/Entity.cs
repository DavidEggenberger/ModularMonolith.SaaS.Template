using Shared.Kernel.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Shared.Features.Domain
{
    public abstract class Entity : IAuditable, IIdentifiable, IConcurrent
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CreatedByUserId { get; set; }

        [ConcurrencyCheck]
        public byte[] RowVersion { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset LastUpdatedAt { get; set; }
    }
}
