using Microsoft.EntityFrameworkCore;
using Modules.Subscription.Features.Infrastructure.Configuration;
using Modules.Subscription.Features.Infrastructure.EFCore;
using Modules.Subscriptions.Features.DomainFeatures.Agregates.StripeSubscriptionAggregate;
using Modules.Subscriptions.IntegrationEvents;
using Shared.Features.CQRS.Command;
using Shared.Features.CQRS.IntegrationEvent;
using Shared.Features.Server;
using Stripe;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptionAggregate.Application.Commands
{
    public class CreateTrialingSubscriptionForTenant : ICommand
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public string StripeCustomerId { get; set; }
        public Stripe.Subscription CreatedStripeSubscription { get; set; }
    }

    public class CreateTrialingSubscriptionCommandHandler : ServerExecutionBase, ICommandHandler<CreateTrialingSubscriptionForTenant>
    {
        private readonly SubscriptionsDbContext subscriptionDbContext;
        private readonly SubscriptionsConfiguration subscriptionConfiguration;
        private readonly IIntegrationEventDispatcher integrationEventDispatcher;

        public CreateTrialingSubscriptionCommandHandler(
            SubscriptionsDbContext subscriptionDbContext,
            SubscriptionsConfiguration subscriptionConfiguration,
            IServiceProvider serviceProvider): base(serviceProvider)
        {
            this.subscriptionDbContext = subscriptionDbContext;
            this.subscriptionConfiguration = subscriptionConfiguration;
        }

        public async Task HandleAsync(CreateTrialingSubscriptionForTenant command, CancellationToken cancellationToken)
        {
            var subscriptionType = subscriptionConfiguration.Subscriptions.First(s => s.StripePriceId == command.CreatedStripeSubscription.Items.First().Price.Id).Type;

            var customer = await new CustomerService().GetAsync(command.CreatedStripeSubscription.CustomerId);
            var stripeCustomer = await subscriptionDbContext.StripeCustomers.FirstAsync(sc => sc.StripePortalCustomerId == customer.Id);

            var tenantId = new Guid(command.CreatedStripeSubscription.Metadata["TenantId"]);

            var stripeSubscription = StripeSubscription.Create(command.CreatedStripeSubscription.TrialEnd, subscriptionType, StripeSubscriptionStatus.Trialing, tenantId, stripeCustomer);

            subscriptionDbContext.StripeSubscriptions.Add(stripeSubscription);

            await subscriptionDbContext.SaveChangesAsync();

            var userSubscriptionUpdatedEvent = new TenantSubscriptionPlanUpdatedIntegrationEvent
            {
                TenantId = tenantId,
                SubscriptionPlanType = subscriptionType
            };

            await integrationEventDispatcher.RaiseAndWaitForCompletionAsync(userSubscriptionUpdatedEvent, cancellationToken);
        }
    }
}
