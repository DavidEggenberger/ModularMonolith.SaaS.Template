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

    public class CreateTenantCommandHandler : ICommandHandler<CreateTenant, Tenant>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;
        public CreateTenantCommandHandler(TenantIdentityDbContext tenantIdentityDbContext)
        {
            this.tenantIdentityDbContext = tenantIdentityDbContext;
        }

        public async Task<Tenant> HandleAsync(CreateTenant createTenant, CancellationToken cancellationToken)
        {
            var tenantsOfUser = await tenantIdentityDbContext.GetAllTenantsForUser(createTenant.CreatorId);


            return new Tenant("");
        }
    }
}
