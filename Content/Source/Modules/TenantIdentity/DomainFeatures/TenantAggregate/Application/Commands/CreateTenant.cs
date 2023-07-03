using FluentValidation;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain;
using Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant;
using Shared.Infrastructure.CQRS.Command;
using Shared.Kernel.BuildingBlocks.ModelValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.DomainFeatures.TenantAggregate.Application.Commands
{
    public class CreateTenant : ICommand<Tenant>
    {
        public string Name { get; set; }
        public Guid CreatorId { get; set; }
    }

    public class CreateTenantValidator : AbstractValidator<CreateTenant>
    {
        public CreateTenantValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be set");
        }
    }

    public class CreateTenantCommandHandler : ICommandHandler<CreateTenant, Tenant>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;
        private readonly ValidationService validationService;
        public CreateTenantCommandHandler(TenantIdentityDbContext tenantIdentityDbContext, ValidationService validationService)
        {
            this.tenantIdentityDbContext = tenantIdentityDbContext;
            this.validationService = validationService;
        }

        public async Task<Tenant> HandleAsync(CreateTenant createTenant, CancellationToken cancellationToken)
        {
            validationService.ThrowIfInvalidModel(createTenant);

            var tenantsOfUser = await tenantIdentityDbContext.GetAllTenantsForUser(createTenant.CreatorId);

            return new Tenant("");
        }
    }
}
