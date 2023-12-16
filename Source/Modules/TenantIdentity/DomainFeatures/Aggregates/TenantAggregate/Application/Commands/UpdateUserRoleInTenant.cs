﻿using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Shared.DomainFeatures.CQRS.Command;
using Shared.Kernel.BuildingBlocks.Authorization.Roles;
using System.Threading;

namespace Modules.TenantIdentity.DomainFeatures.Aggregates.TenantAggregate.Application.Commands
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