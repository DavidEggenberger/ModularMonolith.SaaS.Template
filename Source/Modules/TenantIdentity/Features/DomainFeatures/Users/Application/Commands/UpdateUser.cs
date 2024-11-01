using Microsoft.AspNetCore.Http;
using Shared.Features.Messaging.Command;

namespace Modules.TenantIdentity.Features.DomainFeatures.Users.Application.Commands
{
    public class UpdateUser : Command
    {
        public string UserName { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
