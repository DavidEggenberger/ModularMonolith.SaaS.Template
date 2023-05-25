using Microsoft.AspNetCore.Http;
using Shared.Kernel.Extensions;

namespace Shared.Features.Infrastructure.MultiTenancy.Services
{
    public class TenantResolver : ITenantResolver
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public TenantResolver(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool CanResolveTenant()
        {
            return httpContextAccessor?.HttpContext?.User?.HasTenantIdClaim() == true;
        }

        public Guid ResolveTenantId()
        {
            return httpContextAccessor.HttpContext.User.GetTenantId<Guid>();
        }
    }
}
