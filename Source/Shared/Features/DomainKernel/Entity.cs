using Shared.Features.DomainKernel.Exceptions;
using Shared.Kernel.Interfaces;

namespace Shared.Features.DomainKernel
{
    public abstract class Entity : IAuditable, IIdentifiable, IConcurrent
    {
        public Guid Id { get; set; }
        public Guid CreatedByUserId { get; set; }
        public bool IsSoftDeleted { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastUpdatedAt { get; set; }
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public bool IsDeleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents.Add(eventItem);
        }
        protected void RemoveDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
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
