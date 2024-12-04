using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.IntegrationEvents;
using Shared.Features.Messaging.Command;
using Shared.Features.Server;
using Stripe;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptions.Application.Commands
{
    public class CreateTrialingSubscription : Command
    {
        public Guid TenantId { get; set; }
        public string StripeCustomerId { get; set; }
        public Stripe.Subscription CreatedStripeSubscription { get; set; }
    }

    public class CreateTrialingSubscriptionCommandHandler : ServerExecutionBase<SubscriptionsModule>, ICommandHandler<CreateTrialingSubscription>
    {
        public CreateTrialingSubscriptionCommandHandler(IServiceProvider serviceProvider): base(serviceProvider) { }

        public async Task HandleAsync(CreateTrialingSubscription command, CancellationToken cancellationToken)
        {
            var subscriptionType = module.SubscriptionsConfiguration.Subscriptions.First(s => s.StripePriceId == command.CreatedStripeSubscription.Items.First().Price.Id).Type;

            var customer = await new CustomerService().GetAsync(command.CreatedStripeSubscription.CustomerId);
            var stripeCustomer = await module.SubscriptionsDbContext.StripeCustomers.FirstAsync(sc => sc.StripePortalCustomerId == customer.Id);

            var tenantId = new Guid(command.CreatedStripeSubscription.Metadata["TenantId"]);

            var stripeSubscription = StripeSubscription.Create(
                command.CreatedStripeSubscription.TrialEnd, 
                command.CreatedStripeSubscription.Id, 
                subscriptionType,
                StripeSubscriptionStatus.Trialing, 
                tenantId, 
                stripeCustomer);

            module.SubscriptionsDbContext.StripeSubscriptions.Add(stripeSubscription);

            await module.SubscriptionsDbContext.SaveChangesAsync();

            var userSubscriptionUpdatedEvent = new TenantSubscriptionPlanUpdatedIntegrationEvent
            {
                TenantId = tenantId,
                SubscriptionPlanType = subscriptionType
            };

            await integrationEventDispatcher.RaiseAndWaitForCompletionAsync(userSubscriptionUpdatedEvent, cancellationToken);
        }
    }
}
