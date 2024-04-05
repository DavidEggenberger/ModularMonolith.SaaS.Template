using Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Domain;
using Modules.TenantIdentity.Web.Shared.DTOs.Tenant;
using Shared.Features.Messaging.Command;
using Shared.Features.Server;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Application.Commands
{
    public class CreateTenantWithAdmin : ICommand<TenantDTO>
    {
        public string Name { get; set; }
        public Guid AdminId { get; set; }
    }

    public class CreateTenantWithAdminCommandHandler : ServerExecutionBase<TenantIdentityModule>, ICommandHandler<CreateTenantWithAdmin, TenantDTO>
    {
        public CreateTenantWithAdminCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<TenantDTO> HandleAsync(CreateTenantWithAdmin createTenant, CancellationToken cancellationToken)
        {
            var tenant = Tenant.CreateTenantWithAdmin(createTenant.Name, Guid.Empty);

            module.TenantIdentityDbContext.Tenants.Add(tenant);
            await module.TenantIdentityDbContext.SaveChangesAsync(cancellationToken);

            return tenant.ToDTO();
        }
    }
}
