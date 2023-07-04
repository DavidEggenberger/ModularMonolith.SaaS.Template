using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant.Operations
{
    public class CreateTenantDTO
    {
        public string Name { get; set; }
    }

    public class CreateTenantDTOValidator : AbstractValidator<CreateTenantDTO>
    {
        public CreateTenantDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be set");
        }
    }
}
