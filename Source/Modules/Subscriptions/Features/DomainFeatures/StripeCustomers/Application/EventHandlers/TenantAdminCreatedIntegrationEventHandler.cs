using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.Features.Infrastructure.EFCore;
using Modules.TenantIdentity.Public.IntegrationMessages;
using Shared.Features.Messaging.IntegrationMessages;
using Stripe;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeCustomers.Application.EventHandlers
{
    public class TenantAdminCreatedIntegrationEventHandler : IIntegrationEventHandler<TenantAdminCreatedEvent>
    {
        private readonly SubscriptionsDbContext subscriptionsDbContext;

        public TenantAdminCreatedIntegrationEventHandler(SubscriptionsDbContext subscriptionsDbContext)
        {
            this.subscriptionsDbContext = subscriptionsDbContext;
        }

        public async Task HandleAsync(TenantAdminCreatedEvent tenantAdminCreatedIntegrationEvent, CancellationToken cancellation)
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
