﻿using Microsoft.EntityFrameworkCore;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Shared.DomainFeatures.CQRS.Command;
using Shared.DomainFeatures.DomainKernel.Exceptions;
using System.Threading;

namespace Modules.TenantIdentity.DomainFeatures.Aggregates.TenantAggregate.Application.Commands
{
    public class DeleteTenant : ICommand
    {
        public Guid TenantId { get; set; }
    }

    public class DeleteTenantCommandHandler : ICommandHandler<DeleteTenant>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;

        public DeleteTenantCommandHandler(TenantIdentityDbContext tenantIdentityDbContext)
        {
            this.tenantIdentityDbContext = tenantIdentityDbContext;
        }

        public async Task HandleAsync(DeleteTenant command, CancellationToken cancellationToken)
        {
            var tenant = await tenantIdentityDbContext.Tenants.SingleAsync(t => t.TenantId == command.TenantId);
            if (tenant == null)
            {
                throw new NotFoundException();
            }

            tenant.ThrowIfUserCantDeleteTenant();

            tenantIdentityDbContext.Entry(tenant.Id).State = EntityState.Deleted;
            await tenantIdentityDbContext.SaveChangesAsync();
        }
    }
}