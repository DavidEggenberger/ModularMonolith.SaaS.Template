using Microsoft.AspNetCore.Http;
using Shared.Features.Errors.Exceptions;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Domain.Exceptions
{
    public class TabsAlreadyClosedException : DomainException
    {
        public TabsAlreadyClosedException(string message) : base(message, StatusCodes.Status500InternalServerError)
        {
        }
    }
}
