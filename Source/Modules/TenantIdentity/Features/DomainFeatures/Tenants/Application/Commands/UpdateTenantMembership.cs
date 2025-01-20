using Shared.Features.Messaging.Commands;
using Shared.Features.Misc.ExecutionContext;
using Shared.Kernel.DomainKernel;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.Commands
{
    public class UpdateTenantMembership : Command
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

            tenant.ChangeRoleOfMember(command.UserId, command.Role);

            await module.TenantIdentityDbContext.SaveChangesAsync();
        }
    }
}
