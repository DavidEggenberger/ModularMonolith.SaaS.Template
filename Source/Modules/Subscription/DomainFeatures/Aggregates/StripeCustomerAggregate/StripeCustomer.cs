using Shared.Features.DomainKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Subscriptions.Features.Aggregates.StripeCustomerAggregate
{
    public class StripeCustomer : AggregateRoot
    {
        public string StripePortalCustomerId { get; set; }

        public static StripeCustomer Create(Guid userId, string stripePortalCustomerId)
        {
            return new StripeCustomer
            {
                CreatedAt = DateTime.UtcNow,
                StripePortalCustomerId = stripePortalCustomerId,
                UserId = userId,
            };
        }
    }
}
