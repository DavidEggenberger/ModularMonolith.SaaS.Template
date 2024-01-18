using Microsoft.EntityFrameworkCore;
using Modules.Subscription.Features.Infrastructure.Configuration;
using Modules.Subscription.Features.Infrastructure.EFCore;
using Modules.Subscriptions.Features.Agregates.StripeSubscriptionAggregate;
using Modules.Subscriptions.IntegrationEvents;
using Shared.Features.CQRS.Command;
using Shared.Features.CQRS.IntegrationEvent;
using Stripe;

namespace Modules.Subscriptions.Features.Aggregates.StripeSubscriptionAggregate.Commands
{
    public class CreateTrialingSubscription : ICommand
    {
        public Guid UserId { get; set; }
        public string StripeCustomerId { get; set; }
        public Stripe.Subscription Subscription { get; set; }
    }

    public class CreateTrialingSubscriptionCommandHandler : ICommandHandler<CreateTrialingSubscription>
    {
        private readonly SubscriptionsDbContext subscriptionDbContext;
        private readonly SubscriptionsConfiguration subscriptionConfiguration;
        private readonly IIntegrationEventDispatcher integrationEventDispatcher;

        public CreateTrialingSubscriptionCommandHandler(
            SubscriptionsDbContext subscriptionDbContext,
            SubscriptionsConfiguration subscriptionConfiguration,
            IIntegrationEventDispatcher integrationEventDispatcher)
        {
            this.subscriptionDbContext = subscriptionDbContext;
            this.subscriptionConfiguration = subscriptionConfiguration;
            this.integrationEventDispatcher = integrationEventDispatcher;
        }

        public async Task HandleAsync(CreateTrialingSubscription command, CancellationToken cancellationToken)
        {
            var subscriptionType = subscriptionConfiguration.Subscriptions.First(s => s.StripePriceId == command.Subscription.Items.First().Price.Id).Type;

            var customer = await new CustomerService().GetAsync(command.Subscription.CustomerId);
            var stripeCustomer = await subscriptionDbContext.StripeCustomers.FirstAsync(sc => sc.StripePortalCustomerId == customer.Id);

            var stripeSubscription = StripeSubscription.Create(command.Subscription.TrialEnd, subscriptionType, StripeSubscriptionStatus.Trialing, stripeCustomer);

            subscriptionDbContext.StripeSubscriptions.Add(stripeSubscription);

            await subscriptionDbContext.SaveChangesAsync();

            var userSubscriptionUpdatedEvent = new TenantSubscriptionPlanUpdatedIntegrationEvent
            {
                TenantId = new Guid(command.Subscription.Metadata["TenantId"]),
                SubscriptionPlanType = subscriptionType
            };

            await integrationEventDispatcher.RaiseAndWaitForCompletionAsync(userSubscriptionUpdatedEvent, cancellationToken);
        }
    }
}
