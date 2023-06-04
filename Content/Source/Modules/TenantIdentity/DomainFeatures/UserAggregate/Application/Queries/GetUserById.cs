using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;
using Shared.Infrastructure.CQRS.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.DomainFeatures.UserAggregate.Application.Queries
{
    public class GetUserById : IQuery<User>
    {
        public Guid UserId { get; set; }
    }
}
