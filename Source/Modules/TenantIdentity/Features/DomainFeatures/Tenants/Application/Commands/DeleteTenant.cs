using Microsoft.EntityFrameworkCore;
using System.Threading;
using Shared.Features.Server;
using Modules.TenantIdentity.Features.DomainFeatures.Tenants.Domain;
using Shared.Features.Messaging.Commands;
using Shared.Features.Errors;
using Shared.Kernel.DomainKernel;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.Commands
{
    public class DeleteTenant : Command
    {
        public Guid TenantId { get; set; }
    }

    public class DeleteTenantCommandHandler : ServerExecutionBase<TenantIdentityModule>, ICommandHandler<DeleteTenant>
    {
        public DeleteTenantCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task HandleAsync(DeleteTenant command, CancellationToken cancellationToken)
        {
            var tenant = await module.TenantIdentityDbContext.Tenants.SingleAsync(t => t.Id == command.TenantId);
            if (tenant == null)
            {
                throw Error.NotFound(nameof(Tenant), command.TenantId);
            }

            if (executionContext.TenantRole != TenantRole.Admin)
            {
                throw Error.UnAuthorized;
            }

            module.TenantIdentityDbContext.Entry(tenant.Id).State = EntityState.Deleted;
            await module.TenantIdentityDbContext.SaveChangesAsync();
        }
    }
}
