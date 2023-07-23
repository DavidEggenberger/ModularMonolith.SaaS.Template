using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Shared.Infrastructure.CQRS.Command;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.DomainFeatures.TenantAggregate.Application.Commands
{
    public class CreateTenantMembership : ICommand
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public TenantRole Role { get; set; }
    }
    public class CreateTenantMembershipCommandHandler : ICommandHandler<CreateTenantMembership>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;
        public CreateTenantMembershipCommandHandler(TenantIdentityDbContext tenantIdentityDbContext)
        {
            this.tenantIdentityDbContext = tenantIdentityDbContext;
        }

        public async Task HandleAsync(CreateTenantMembership command, CancellationToken cancellationToken)
        {
            var tenant = await tenantIdentityDbContext.GetTenantByIdAsync(command.TenantId);

            tenant.AddUser(command.UserId, command.Role);

            await tenantIdentityDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
