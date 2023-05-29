using Modules.TenantIdentity.Features.UserAggregate.Domain;
using Shared.Features.Infrastructure.CQRS.Query;

namespace Shared.Modules.TenantIdentityModule.Application.Queries
{
    public class UserByStripeCustomerIdQuery : IQuery<ApplicationUser>
    {
        public string StripeCustomerId { get; set; }
    }
}
