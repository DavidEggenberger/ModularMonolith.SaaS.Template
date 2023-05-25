namespace Shared.Features.Infrastructure.MultiTenancy.Services
{
    public interface ITenantResolver
    {
        bool CanResolveTenant();
        Guid ResolveTenantId();
    }
}
