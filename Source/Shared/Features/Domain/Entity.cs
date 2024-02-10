using Shared.Features.Domain.Exceptions;
using Shared.Kernel.Interfaces;

namespace Shared.Features.Domain
{
    public abstract class Entity : IAuditable, IIdentifiable, IConcurrent
    {
        public Guid Id { get; set; }
        public Guid CreatedByUserId { get; set; }
        public bool IsSoftDeleted { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastUpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public void SoftDelete()
        {
            if (IsSoftDeleted is true)
            {
                throw new InvalidEntityDeleteException("Can't delete an already deleted entity");
            }
            else
            {
                IsSoftDeleted = true;
            }
        }
        public void UndoSoftDelete()
        {
            IsSoftDeleted = false;
        }
    }
}
