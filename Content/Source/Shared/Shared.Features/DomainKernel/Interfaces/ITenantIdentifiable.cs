namespace Shared.Features.DomainKernel.Interfaces
{
    public interface ITenantIdentifiable
    {
        Guid TenantId { get; set; }
    }
}
