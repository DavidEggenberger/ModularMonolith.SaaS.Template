namespace Shared.DomainFeatures.Infrastructure.MultiTenancy.Exceptions
{
    public class EntityNotTenantIdentifiableException : Exception
    {
        public EntityNotTenantIdentifiableException(string message) : base(message)
        {

        }
    }
}
