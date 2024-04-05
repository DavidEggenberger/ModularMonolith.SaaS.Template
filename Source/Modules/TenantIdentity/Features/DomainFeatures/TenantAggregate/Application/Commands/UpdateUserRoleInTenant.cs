using Shared.Features.Messaging.Command;
using Shared.Features.Server;
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
    public class UpdateTenantMembershipCommandHandler : ServerExecutionBase<TenantIdentityModule>, ICommandHandler<UpdateTenantMembership>
    {
        public UpdateTenantMembershipCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public async Task HandleAsync(UpdateTenantMembership command, CancellationToken cancellationToken)
        {
            var tenant = await module.TenantIdentityDbContext.GetTenantExtendedByIdAsync(command.TenantId);

            tenant.ChangeRoleOfTenantMember(command.UserId, command.Role);

            await module.TenantIdentityDbContext.SaveChangesAsync();
        }
    }
}
