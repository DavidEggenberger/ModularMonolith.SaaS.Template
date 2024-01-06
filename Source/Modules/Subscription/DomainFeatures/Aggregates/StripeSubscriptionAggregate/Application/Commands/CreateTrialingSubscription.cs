using Microsoft.EntityFrameworkCore;
using Modules.Subscription.Features.Infrastructure.Configuration;
using Modules.Subscription.Features.Infrastructure.EFCore;
using Modules.Subscription.IntegrationEvents;
using Modules.Subscriptions.Features.Aggregates.StripeSubscriptionAggregate;
using Shared.DomainFeatures.CQRS.Command;
using Shared.Features.CQRS.Command;
using Shared.Features.CQRS.IntegrationEvent;
using Shared.Kernel.BuildingBlocks.Auth;
using Stripe;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Subscriptions.Features.Aggregates.StripeSubscriptionAggregate.Application.Commands
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

            var userSubscriptionUpdatedEvent = new UserSubscriptionPlanUpdatedIntegrationEvent
            {
                SubscriptionPlanType = subscriptionType,
                UserId = command.UserId
            };

            await integrationEventDispatcher.RaiseAndWaitForCompletionAsync(userSubscriptionUpdatedEvent, cancellationToken);
        }
    }
}
