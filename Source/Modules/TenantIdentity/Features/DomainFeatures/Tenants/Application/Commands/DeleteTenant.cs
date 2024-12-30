using Microsoft.EntityFrameworkCore;
using System.Threading;
using Shared.Features.Server;
using Shared.Kernel.Errors;
using Modules.TenantIdentity.Features.DomainFeatures.Tenants.Domain;
using Shared.Features.Messaging.Commands;

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
                throw Errors.NotFound(nameof(Tenant), command.TenantId);
            }

            tenant.ThrowIfUserCantDeleteTenant();

            module.TenantIdentityDbContext.Entry(tenant.Id).State = EntityState.Deleted;
            await module.TenantIdentityDbContext.SaveChangesAsync();
        }
    }
}
