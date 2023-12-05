using Microsoft.AspNetCore.Http;
using Shared.Infrastructure.CQRS.Command;

namespace Modules.TenantIdentity.DomainFeatures.Aggregates.UserAggregate.Application.Commands
{
    public class UpdateUser : ICommand
    {
        public string UserName { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
