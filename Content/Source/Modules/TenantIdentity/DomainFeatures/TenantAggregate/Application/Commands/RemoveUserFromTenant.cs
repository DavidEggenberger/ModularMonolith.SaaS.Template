﻿using Microsoft.EntityFrameworkCore;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Shared.Infrastructure.CQRS.Command;
using Shared.Kernel.BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.DomainFeatures.TenantAggregate.Application.Commands
{
    public class RemoveUserFromTenant : ICommand
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }

    public class RemoveUserFromTenantommandHandler : ICommandHandler<RemoveUserFromTenant>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;
        private readonly IExecutionContextAccessor executionContextAccessor;

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