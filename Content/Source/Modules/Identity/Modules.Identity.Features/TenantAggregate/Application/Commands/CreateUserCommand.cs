using Microsoft.AspNetCore.Identity;
using Modules.TenantIdentityModule.Domain;
using Shared.Infrastructure.CQRS.Command;

namespace Shared.Modules.Layers.Infrastructure.Identity.Commands
{
    public class CreateUserCommand : ICommand
    {
        public ApplicationUser User { get; set; }
        public ExternalLoginInfo LoginInfo { get; set; }
    }
}
