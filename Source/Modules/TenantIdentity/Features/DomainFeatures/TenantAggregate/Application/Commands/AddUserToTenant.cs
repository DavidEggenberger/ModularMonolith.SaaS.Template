using Shared.Features.EFCore;
using Shared.Features.Messaging.Command;
using Shared.Features.Server;
using Shared.Kernel.BuildingBlocks.Auth;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Application.Commands
{
    public class AddUserToTenant : ICommand
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public TenantRole Role { get; set; }
    }
    public class AddUserToTenantCommandHandler : ServerExecutionBase<TenantIdentityModule>, ICommandHandler<AddUserToTenant>
    {
        public AddUserToTenantCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task HandleAsync(AddUserToTenant command, CancellationToken cancellationToken)
        {
            var tenant = await module.TenantIdentityDbContext.Tenants.GetAggregateRootAsync(command.TenantId, command.TenantId);

            tenant.AddUser(command.UserId, command.Role);

            await module.TenantIdentityDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
