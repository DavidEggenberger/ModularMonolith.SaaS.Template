namespace Shared.DomainFeatures.MultiTenancy.Services
{
    public interface ITenantResolver
    {
        bool CanResolveTenant();
        Guid ResolveTenantId();
    }
}
