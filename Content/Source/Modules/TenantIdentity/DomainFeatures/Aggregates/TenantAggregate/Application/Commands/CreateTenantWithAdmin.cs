using Modules.TenantIdentity.DomainFeatures.Aggregates.TenantAggregate.Domain;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant;
using Shared.Infrastructure.CQRS.Command;
using System.Threading;

namespace Modules.TenantIdentity.DomainFeatures.Aggregates.TenantAggregate.Application.Commands
{
    public class CreateTenantWithAdmin : ICommand<TenantDTO>
    {
        public string Name { get; set; }
        public Guid AdminId { get; set; }
    }

    public class CreateTenantWithAdminCommandHandler : ICommandHandler<CreateTenantWithAdmin, TenantDTO>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;
        public CreateTenantWithAdminCommandHandler(TenantIdentityDbContext tenantIdentityDbContext)
        {
            this.tenantIdentityDbContext = tenantIdentityDbContext;
        }

        public async Task<TenantDTO> HandleAsync(CreateTenantWithAdmin createTenant, CancellationToken cancellationToken)
        {
            var tenant = await Tenant.CreateTenantWithAdminAsync(createTenant.Name, Guid.Empty);

            tenantIdentityDbContext.Tenants.Add(tenant);
            await tenantIdentityDbContext.SaveChangesAsync(cancellationToken);

            return tenant.ToDTO();
        }
    }
}
