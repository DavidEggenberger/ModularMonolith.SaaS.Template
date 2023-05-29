using Microsoft.AspNetCore.Identity;
using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;
using Shared.DomainFeatures.Infrastructure.CQRS.Command;

namespace Modules.TenantIdentity.DomainFeatures.Application.Commands
{
    public class CreateUserCommand : ICommand
    {
        public ApplicationUser User { get; set; }
        public ExternalLoginInfo LoginInfo { get; set; }
    }
}
