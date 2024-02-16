using Modules.TenantIdentity.Features.Infrastructure.EFCore;
using Shared.Features.CQRS.Command;
using Shared.Kernel.BuildingBlocks;
using System.Threading;

namespace Modules.TenantIdentity.Features.Aggregates.TenantAggregate.Application.Commands
{
    public class RemoveUserFromTenant : ICommand
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }

    public class RemoveUserFromTenantommandHandler : ICommandHandler<RemoveUserFromTenant>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;
        private readonly IExecutionContext executionContext;

        public RemoveUserFromTenantommandHandler(TenantIdentityDbContext tenantIdentityDbContext)
        {
            this.tenantIdentityDbContext = tenantIdentityDbContext;
        }

        public async Task HandleAsync(RemoveUserFromTenant command, CancellationToken cancellationToken)
        {
            var tenant = await tenantIdentityDbContext.GetTenantExtendedByIdAsync(command.TenantId);

            tenant.DeleteTenantMembership(command.UserId);

            tenantIdentityDbContext.Remove(tenant);
            await tenantIdentityDbContext.SaveChangesAsync();
        }
    }
}
