using Microsoft.AspNetCore.Identity;
using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;
using Shared.Infrastructure.CQRS.Command;

namespace Modules.TenantIdentity.DomainFeatures.UserAggregate.Application.Commands
{
    public class CreateNewUser : ICommand
    {
        public User User { get; set; }
        public ExternalLoginInfo LoginInfo { get; set; }
    }

}
