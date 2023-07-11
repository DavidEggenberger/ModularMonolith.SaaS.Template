using Microsoft.EntityFrameworkCore;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Shared.Infrastructure.CQRS.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.DomainFeatures.TenantAggregate.Application.Commands
{
    public class DeleteTenantMembership : ICommand
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }

    public class DeleteTenantMembershipCommandHandler : ICommandHandler<DeleteTenantMembership>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;

        public DeleteTenantMembershipCommandHandler(TenantIdentityDbContext tenantIdentityDbContext)
        {
            this.tenantIdentityDbContext = tenantIdentityDbContext;
        }

        public async Task HandleAsync(DeleteTenantMembership command, CancellationToken cancellationToken)
        {
            

            tenantIdentityDbContext.Entry(tenant.Id).State = EntityState.Deleted;
            await tenantIdentityDbContext.SaveChangesAsync();
        }
    }
}
