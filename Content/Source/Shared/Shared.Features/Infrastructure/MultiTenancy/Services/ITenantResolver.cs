namespace Shared.Infrastructure.MultiTenancy.Services
{
    public interface ITenantResolver
    {
        bool CanResolveTenant();
        Guid ResolveTenantId();
    }
}
