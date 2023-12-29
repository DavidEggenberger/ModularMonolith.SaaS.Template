namespace Modules.Subscriptions.Features.StripeSubscriptionAggregate
{
    public class StripeCustomer
    {
        public Guid UserId { get; set; }
        public string StripeCustomerId { get; set; }
    }
}
