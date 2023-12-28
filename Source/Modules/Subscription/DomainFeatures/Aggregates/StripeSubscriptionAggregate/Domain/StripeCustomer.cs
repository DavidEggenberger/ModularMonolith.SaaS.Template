namespace Modules.Subscription.Features.Aggregates.StripeSubscriptionAggregate.Domain
{
    public class StripeCustomer
    {
        public Guid UserId { get; set; }
        public string StripeCustomerId { get; set; }
    }
}
