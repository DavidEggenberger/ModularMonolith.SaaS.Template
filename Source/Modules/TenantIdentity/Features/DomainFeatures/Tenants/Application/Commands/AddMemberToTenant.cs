using Shared.Features.EFCore;
using Shared.Features.Messaging.Commands;
using Shared.Features.Misc.ExecutionContext;
using Shared.Kernel.DomainKernel;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.Commands
{
    public class AddMemberToTenant : Command
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public TenantRole Role { get; set; }
    }
    public class AddMemberToTenantCommandHandler : ServerExecutionBase<TenantIdentityModule>, ICommandHandler<AddMemberToTenant>
    {
        public AddMemberToTenantCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task HandleAsync(AddMemberToTenant command, CancellationToken cancellationToken)
        {
            var tenant = await module.TenantIdentityDbContext.Tenants.GetEntityAsync(command.TenantId, command.TenantId);

            tenant.AddMember(command.UserId, command.Role);

            await module.TenantIdentityDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
