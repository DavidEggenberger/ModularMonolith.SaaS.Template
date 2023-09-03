namespace Modules.TenantIdentity.DomainFeatures.Aggregates.TenantAggregate.Domain.Exceptions
{
    public class IdentityOperationException : Exception
    {
        public IdentityOperationException(string message = "") : base(message)
        {

        }
    }
}
