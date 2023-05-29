namespace Modules.TenantIdentity.DomainFeatures.Domain.Exceptions
{
    public class IdentityOperationException : Exception
    {
        public IdentityOperationException(string message = "") : base(message)
        {

        }
    }
}
