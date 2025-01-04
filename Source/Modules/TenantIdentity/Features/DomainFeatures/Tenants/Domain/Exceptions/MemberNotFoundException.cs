using Microsoft.AspNetCore.Http;
using Shared.Features.Errors.Exceptions;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Domain.Exceptions
{
    public class MemberNotFoundException : DomainException
    {
        public MemberNotFoundException() : base("Member not found", StatusCodes.Status404NotFound)
        {
        }
    }
}
