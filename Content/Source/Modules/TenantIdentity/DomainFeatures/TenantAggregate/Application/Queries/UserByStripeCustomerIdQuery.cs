using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;
using Shared.Infrastructure.CQRS.Query;

namespace Modules.TenantIdentity.DomainFeatures.Application.Queries
{
    public class UserByStripeCustomerIdQuery : IQuery<ApplicationUser>
    {
        public string StripeCustomerId { get; set; }
    }
}
