using Shared.Features.Messaging.Command;
using Shared.Features.Server;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.Users.Application.Commands
{
    public class SetSelectedTenantForUser : Command
    {
        public Guid SelectedTenantId { get; set; }
    }

    public class SetSelectedTenantForUserHandler : ServerExecutionBase<TenantIdentityModule>, ICommandHandler<SetSelectedTenantForUser>
    {
        public SetSelectedTenantForUserHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task HandleAsync(SetSelectedTenantForUser command, CancellationToken cancellationToken)
        {
            var user = await module.TenantIdentityDbContext.GetUserByIdAsync(command.ExecutingUserId);

            if (user.SelectedTenantId == command.SelectedTenantId)
            {
                return;
            }

            user.SelectedTenantId = command.SelectedTenantId;

            await module.TenantIdentityDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
