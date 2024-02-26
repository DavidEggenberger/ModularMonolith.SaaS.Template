using Modules.TenantIdentity.Features.Infrastructure.EFCore;
using Shared.Features.CQRS.Command;
using Shared.Kernel.BuildingBlocks.Auth;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Application.Commands
{
    public class UpdateTenantMembership : ICommand
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public TenantRole Role { get; set; }
    }
    public class UpdateTenantMembershipCommandHandler : ICommandHandler<UpdateTenantMembership>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;
        public UpdateTenantMembershipCommandHandler(TenantIdentityDbContext tenantIdentityDbContext)
        {
            this.tenantIdentityDbContext = tenantIdentityDbContext;
        }
        public async Task HandleAsync(UpdateTenantMembership command, CancellationToken cancellationToken)
        {
            var tenant = await tenantIdentityDbContext.GetTenantExtendedByIdAsync(command.TenantId);

            tenant.ChangeRoleOfTenantMember(command.UserId, command.Role);

            await tenantIdentityDbContext.SaveChangesAsync();
        }
    }
}
