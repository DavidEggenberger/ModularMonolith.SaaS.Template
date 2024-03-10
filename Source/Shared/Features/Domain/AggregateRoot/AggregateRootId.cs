namespace Shared.Features.Domain.AggregateRoot
{
    public abstract class AggregateRootId : ValueObject
    {
        public Guid Value { get; protected set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
