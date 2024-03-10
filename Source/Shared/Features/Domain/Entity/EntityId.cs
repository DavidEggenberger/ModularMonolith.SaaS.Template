namespace Shared.Features.Domain.Entity
{
    public class EntityId : ValueObject
    {
        public Guid Value { get; protected set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
