using Microsoft.AspNetCore.Http;
using Shared.Features.CQRS.Command;

namespace Modules.TenantIdentity.DomainFeatures.UserAggregate.Application.Commands
{
    public class UpdateUser : ICommand
    {
        public string UserName { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
