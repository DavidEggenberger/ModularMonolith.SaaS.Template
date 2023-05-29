namespace Shared.DomainFeatures.Infrastructure.MultiTenancy.Services
{
    public interface ITenantResolver
    {
        bool CanResolveTenant();
        Guid ResolveTenantId();
    }
}
