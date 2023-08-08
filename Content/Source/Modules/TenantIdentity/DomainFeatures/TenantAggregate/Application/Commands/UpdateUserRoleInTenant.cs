using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Shared.Infrastructure.CQRS.Command;
using Shared.Kernel.BuildingBlocks.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.DomainFeatures.TenantAggregate.Application.Commands
{
    public class UpdateTenantMembership : ICommand
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public TenantRole Role { get; set; }
    }
    public class UpdateTenantMembershipCommandHandler : ICommandHandler<UpdateTenantMembership>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;
        public UpdateTenantMembershipCommandHandler(TenantIdentityDbContext tenantIdentityDbContext)
        {
            this.tenantIdentityDbContext = tenantIdentityDbContext;
        }
        public async Task HandleAsync(UpdateTenantMembership command, CancellationToken cancellationToken)
        {
            var tenant = await tenantIdentityDbContext.GetTenantExtendedByIdAsync(command.TenantId);

            tenant.ChangeRoleOfUser(command.UserId, command.Role);

            await tenantIdentityDbContext.SaveChangesAsync();
        }
    }
}
