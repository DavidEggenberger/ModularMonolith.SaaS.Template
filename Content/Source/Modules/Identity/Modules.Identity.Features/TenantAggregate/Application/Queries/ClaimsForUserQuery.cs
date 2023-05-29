using Modules.IdentityModule.Domain;
using Modules.TenantIdentity.Features.UserAggregate.Domain;
using Shared.Features.Infrastructure.CQRS.Query;
using System.Security.Claims;

namespace Shared.Modules.TenantIdentityModule.Application.Queries
{
    public class ClaimsForUserQuery : IQuery<IEnumerable<Claim>>
    {
        public ApplicationUser User { get; set; }
    }
}
