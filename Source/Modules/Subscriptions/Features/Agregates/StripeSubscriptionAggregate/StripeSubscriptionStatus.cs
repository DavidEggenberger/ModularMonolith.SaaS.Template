namespace Modules.Subscriptions.Features.Agregates.StripeSubscriptionAggregate
{
    public enum StripeSubscriptionStatus
    {
        Active,
        Canceled,
        Trialing,
        Unpaid
    }
}
