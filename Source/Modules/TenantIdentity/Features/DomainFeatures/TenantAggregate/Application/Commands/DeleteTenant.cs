using Microsoft.EntityFrameworkCore;
using Shared.Features.Messaging.Command;
using Shared.Features.Domain.Exceptions;
using System.Threading;
using Shared.Features.Server;

namespace Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Application.Commands
{
    public class DeleteTenant : ICommand
    {
        public Guid TenantId { get; set; }
    }

    public class DeleteTenantCommandHandler : ServerExecutionBase<TenantIdentityModule>, ICommandHandler<DeleteTenant>
    {
        public DeleteTenantCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task HandleAsync(DeleteTenant command, CancellationToken cancellationToken)
        {
            var tenant = await module.TenantIdentityDbContext.Tenants.SingleAsync(t => t.TenantId == command.TenantId);
            if (tenant == null)
            {
                throw new NotFoundException();
            }

            tenant.ThrowIfUserCantDeleteTenant();

            module.TenantIdentityDbContext.Entry(tenant.Id).State = EntityState.Deleted;
            await module.TenantIdentityDbContext.SaveChangesAsync();
        }
    }
}
