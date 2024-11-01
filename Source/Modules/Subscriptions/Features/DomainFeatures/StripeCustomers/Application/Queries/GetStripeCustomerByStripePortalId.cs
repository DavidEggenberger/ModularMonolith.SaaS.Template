using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.Features.Infrastructure.EFCore;
using Shared.Features.Messaging.Query;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeCustomers.Application.Queries
{
    public class GetStripeCustomerByStripePortalId : Query<StripeCustomer>
    {
        public string StripeCustomerStripePortalId { get; set; }
    }

    public class GetStripeCustomerByStripePortalIdQueryHandler : IQueryHandler<GetStripeCustomerByStripePortalId, StripeCustomer>
    {
        private readonly SubscriptionsDbContext subscriptionsDbContext;

        public GetStripeCustomerByStripePortalIdQueryHandler(SubscriptionsDbContext subscriptionsDbContext)
        {
            this.subscriptionsDbContext = subscriptionsDbContext;
        }

        public async Task<StripeCustomer> HandleAsync(GetStripeCustomerByStripePortalId query, CancellationToken cancellation)
        {
            var stripeCustomer = await subscriptionsDbContext.StripeCustomers.FirstAsync(c => c.StripePortalCustomerId == query.StripeCustomerStripePortalId);

            return stripeCustomer;
        }
    }
}
