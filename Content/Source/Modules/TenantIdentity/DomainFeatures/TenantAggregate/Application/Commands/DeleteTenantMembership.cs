using Shared.Infrastructure.CQRS.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.DomainFeatures.TenantAggregate.Application.Commands
{
    public class DeleteTenantMembership : ICommand
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
}
