using Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Modules.TenantIdentity.Web.Shared.DTOs.Tenant;
using Shared.Features.CQRS.Command;
using System.Threading;

namespace Modules.TenantIdentity.DomainFeatures.TenantAggregate.Application.Commands
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
            var tenant = Tenant.CreateTenantWithAdmin(createTenant.Name, Guid.Empty);

            tenantIdentityDbContext.Tenants.Add(tenant);
            await tenantIdentityDbContext.SaveChangesAsync(cancellationToken);

            return tenant.ToDTO();
        }
    }
}
