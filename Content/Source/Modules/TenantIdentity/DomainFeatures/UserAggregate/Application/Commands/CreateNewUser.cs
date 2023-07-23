using Microsoft.AspNetCore.Identity;
using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;
using Shared.Infrastructure.CQRS.Command;
using System.Threading;

namespace Modules.TenantIdentity.DomainFeatures.UserAggregate.Application.Commands
{
    public class CreateNewUser : ICommand
    {
        public User User { get; set; }
        public ExternalLoginInfo LoginInfo { get; set; }
    }
    public class CreateNewUserCommandHandler : ICommandHandler<CreateNewUser>
    {
        private readonly UserManager<User> userManager;
        public CreateNewUserCommandHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task HandleAsync(CreateNewUser command, CancellationToken cancellationToken)
        {
            await userManager.CreateAsync(command.User);
        }
    }
}
