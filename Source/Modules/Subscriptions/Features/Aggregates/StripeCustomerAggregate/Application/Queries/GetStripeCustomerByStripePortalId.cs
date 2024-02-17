using Microsoft.EntityFrameworkCore;
using Modules.Subscription.Features.Infrastructure.EFCore;
using Modules.Subscriptions.Features.Agregates.StripeCustomerAggregate;
using Shared.Features.CQRS.Query;

namespace Modules.Subscriptions.Features.Aggregates.StripeCustomerAggregate.Application.Queries
{
    public class GetStripeCustomerByStripePortalId : IQuery<StripeCustomer>
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
