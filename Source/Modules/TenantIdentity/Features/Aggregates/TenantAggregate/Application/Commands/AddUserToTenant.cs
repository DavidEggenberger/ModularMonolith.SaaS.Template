using Modules.TenantIdentity.Features.Infrastructure.EFCore;
using Shared.Features.CQRS.Command;
using Shared.Kernel.BuildingBlocks.Authorization.Roles;
using System.Threading;

namespace Modules.TenantIdentity.Features.Aggregates.TenantAggregate.Application.Commands
{
    public class AddUserToTenant : ICommand
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public TenantRole Role { get; set; }
    }
    public class AddUserToTenantCommandHandler : ICommandHandler<AddUserToTenant>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;
        public AddUserToTenantCommandHandler(TenantIdentityDbContext tenantIdentityDbContext)
        {
            this.tenantIdentityDbContext = tenantIdentityDbContext;
        }

        public async Task HandleAsync(AddUserToTenant command, CancellationToken cancellationToken)
        {
            var tenant = await tenantIdentityDbContext.GetTenantByIdAsync(command.TenantId);

            tenant.AddUser(command.UserId, command.Role);

            await tenantIdentityDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
