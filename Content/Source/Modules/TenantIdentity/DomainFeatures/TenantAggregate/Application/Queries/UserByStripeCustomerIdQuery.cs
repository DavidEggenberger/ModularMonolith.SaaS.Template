using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;
using Shared.DomainFeatures.Infrastructure.CQRS.Query;

namespace Modules.TenantIdentity.DomainFeatures.Application.Queries
{
    public class UserByStripeCustomerIdQuery : IQuery<ApplicationUser>
    {
        public string StripeCustomerId { get; set; }
    }
}
