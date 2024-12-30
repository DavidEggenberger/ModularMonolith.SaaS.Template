using FluentValidation;

namespace Modules.TenantIdentity.Public.DTOs.Tenant.Operations
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
