namespace Shared.Features.MultiTenancy.Services
{
    public interface ITenantResolver
    {
        bool CanResolveTenant();
        Guid ResolveTenantId();
    }
}
