using Microsoft.EntityFrameworkCore;
using Modules.TenantIdentity.DomainFeatures.Application.Queries;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant;
using Shared.Infrastructure.CQRS.Command;
using Shared.Infrastructure.CQRS.Query;
using Shared.Infrastructure.DomainKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.DomainFeatures.TenantAggregate.Application.Commands
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
