using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.Features.Infrastructure.EFCore;
using Modules.TenantIdentity.IntegrationEvents;
using Shared.Features.Messaging.IntegrationEvent;
using Stripe;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeCustomerAggregate.Application.IntegrationEvents
{
    public class TenantAdminCreatedIntegrationEventHandler : IIntegrationEventHandler<TenantAdminCreatedIntegrationEvent>
    {
        private readonly SubscriptionsDbContext subscriptionsDbContext;

        public TenantAdminCreatedIntegrationEventHandler(SubscriptionsDbContext subscriptionsDbContext)
        {
            this.subscriptionsDbContext = subscriptionsDbContext;
        }

        public async Task HandleAsync(TenantAdminCreatedIntegrationEvent tenantAdminCreatedIntegrationEvent, CancellationToken cancellation)
        {
            var stripeCustomer = await subscriptionsDbContext.StripeCustomers.FirstOrDefaultAsync(stripeCustomer => stripeCustomer.UserId == tenantAdminCreatedIntegrationEvent.UserId);
            if (stripeCustomer == null)
            {
                var stripeCustomerService = new CustomerService();

                var options = new CustomerCreateOptions
                {
                    Email = tenantAdminCreatedIntegrationEvent.Email,
                    Metadata = new Dictionary<string, string>
                    {
                        ["UserId"] = tenantAdminCreatedIntegrationEvent.UserId.ToString()
                    }
                };

                var customer = await stripeCustomerService.CreateAsync(options);


            }
        }
    }
}
