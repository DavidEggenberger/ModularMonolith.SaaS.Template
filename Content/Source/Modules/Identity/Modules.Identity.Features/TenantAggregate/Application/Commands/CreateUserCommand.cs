using Microsoft.AspNetCore.Identity;
using Modules.TenantIdentity.Features.UserAggregate.Domain;
using Shared.Features.Infrastructure.CQRS.Command;

namespace Shared.Modules.Layers.Infrastructure.Identity.Commands
{
    public class CreateUserCommand : ICommand
    {
        public ApplicationUser User { get; set; }
        public ExternalLoginInfo LoginInfo { get; set; }
    }
}
