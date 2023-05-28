using Modules.TenantIdentityModule.Domain;
using Shared.Infrastructure.CQRS.Query;

namespace Shared.Modules.TenantIdentityModule.Application.Queries
{
    public class UserByStripeCustomerIdQuery : IQuery<ApplicationUser>
    {
        public string StripeCustomerId { get; set; }
    }
}
