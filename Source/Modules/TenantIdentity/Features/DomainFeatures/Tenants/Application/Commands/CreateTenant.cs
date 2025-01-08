using Modules.TenantIdentity.Features.DomainFeatures.Tenants.Domain;
using Modules.TenantIdentity.Public.DTOs.Tenant;
using Shared.Features.Messaging.Commands;
using Shared.Features.Misc;
using Shared.Features.Misc.ExecutionContext;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.Commands
{
    public class CreateTenant : Command<TenantDTO>
    {
        public string Name { get; set; }
        public Guid AdminId { get; set; }
    }

    public class CreateTenantWithAdminCommandHandler : ServerExecutionBase<TenantIdentityModule>, ICommandHandler<CreateTenant, TenantDTO>
    {
        public CreateTenantWithAdminCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<TenantDTO> HandleAsync(CreateTenant createTenant, CancellationToken cancellationToken)
        {
            var tenant = Tenant.CreateTenant(createTenant.Name, Guid.Empty);

            module.TenantIdentityDbContext.Tenants.Add(tenant);
            await module.TenantIdentityDbContext.SaveChangesAsync(cancellationToken);

            return tenant.ToDTO();
        }
    }
}
