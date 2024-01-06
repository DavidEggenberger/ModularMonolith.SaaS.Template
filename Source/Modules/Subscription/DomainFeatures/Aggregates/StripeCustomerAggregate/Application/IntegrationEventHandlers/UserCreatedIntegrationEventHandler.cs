using Modules.Identity.IntegrationEvents;
using Modules.Subscription.Features.Infrastructure.EFCore;
using Shared.Features.CQRS.IntegrationEvent;
using Stripe;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Subscriptions.Features.Aggregates.StripeCustomerAggregate.Application.IntegrationEventHandlers
{
    public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly SubscriptionsDbContext subscriptionsDbContext;
        public UserCreatedIntegrationEventHandler(SubscriptionsDbContext subscriptionsDbContext)
        {
            this.subscriptionsDbContext = subscriptionsDbContext;
        }

        public async Task HandleAsync(UserCreatedIntegrationEvent userCreatedIntegrationEvent, CancellationToken cancellation)
        {
            var stripeCustomerService = new CustomerService();

            var options = new CustomerCreateOptions
            {
                Email = userCreatedIntegrationEvent.Email,
                Metadata = new Dictionary<string, string>
                {
                    ["UserId"] = userCreatedIntegrationEvent.UserId.ToString()
                }
            };

            var customer = await stripeCustomerService.CreateAsync(options);

            var stripeCustomer = StripeCustomer.Create(userCreatedIntegrationEvent.UserId, customer.Id);

            subscriptionsDbContext.StripeCustomers.Add(stripeCustomer);

            await subscriptionsDbContext.SaveChangesAsync();
        }
    }
}
