using Shared.Features.Messaging.Commands;
using Shared.Features.Server;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.Commands
{
    public class RemoveMemberFromTenant : Command
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }

    public class RemoveMemberFromTenantCommandHandler : ServerExecutionBase<TenantIdentityModule>, ICommandHandler<RemoveMemberFromTenant>
    {
        public RemoveMemberFromTenantCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider) {}

        public async Task HandleAsync(RemoveMemberFromTenant command, CancellationToken cancellationToken)
        {
            var tenant = await module.TenantIdentityDbContext.GetTenantExtendedByIdAsync(command.TenantId);

            tenant.RemoveMember(command.UserId);

            module.TenantIdentityDbContext.Remove(tenant);
            await module.TenantIdentityDbContext.SaveChangesAsync();
        }
    }
}
