using Shared.Features.Messaging.Commands;
using Shared.Features.Server;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.Users.Application.Commands
{
    public class SelectTenant : Command
    {
        public Guid SelectedTenantId { get; set; }
    }

    public class SelectedTenantCommandHandler : ServerExecutionBase<TenantIdentityModule>, ICommandHandler<SelectTenant>
    {
        public SelectedTenantCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task HandleAsync(SelectTenant command, CancellationToken cancellationToken)
        {
            var user = await module.TenantIdentityDbContext.GetUserByIdAsync(command.ExecutingUserId);

            if (user.SelectedTenantId == command.SelectedTenantId)
            {
                return;
            }

            user.SelectTenant(command.SelectedTenantId);

            await module.TenantIdentityDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
