namespace Modules.Subscriptions.Features.Agregates.StripeSubscriptionAggregate
{
    public enum SubscriptionStatus
    {
        Active,
        Canceled,
        Trialing,
        Unpaid
    }
}
