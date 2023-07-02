using Shared.Infrastructure.CQRS.Command;
using Shared.Kernel.BuildingBlocks.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.DomainFeatures.TenantAggregate.Application.Commands
{
    public class CreateTenantMembership : ICommand
    {
        public TenantRole Role { get; set; }
    }
}
