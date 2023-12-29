namespace Modules.Subscriptions.Features.StripeSubscriptionAggregate
{
    public enum StripeSubscriptionStatus
    {
        Active,
        Canceled,
        Trialing,
        Unpaid
    }
}
