namespace Shared.Features.EFCore.MultiTenancy.Exceptions
{
    public class EntityNotTenantIdentifiableException : Exception
    {
        public EntityNotTenantIdentifiableException(string message) : base(message)
        {

        }
    }
}
