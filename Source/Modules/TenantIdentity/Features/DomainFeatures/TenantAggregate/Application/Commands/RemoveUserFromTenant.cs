using Shared.Features.Messaging.Command;
using Shared.Features.Server;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Application.Commands
{
    public class RemoveUserFromTenant : ICommand
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }

    public class RemoveUserFromTenantommandHandler : ServerExecutionBase<TenantIdentityModule>, ICommandHandler<RemoveUserFromTenant>
    {
        public RemoveUserFromTenantommandHandler(IServiceProvider serviceProvider) : base(serviceProvider) {}

        public async Task HandleAsync(RemoveUserFromTenant command, CancellationToken cancellationToken)
        {
            var tenant = await module.TenantIdentityDbContext.GetTenantExtendedByIdAsync(command.TenantId);

            tenant.DeleteTenantMembership(command.UserId);

            module.TenantIdentityDbContext.Remove(tenant);
            await module.TenantIdentityDbContext.SaveChangesAsync();
        }
    }
}
