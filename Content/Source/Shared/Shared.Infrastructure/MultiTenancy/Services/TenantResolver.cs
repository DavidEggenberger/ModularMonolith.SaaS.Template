using Microsoft.AspNetCore.Http;
using Shared.SharedKernel.Exstensions;

namespace Shared.Infrastructure.MultiTenancy.Services
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
