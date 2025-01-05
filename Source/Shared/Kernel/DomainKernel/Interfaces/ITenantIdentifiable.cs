namespace Shared.Kernel.DomainKernel.Interfaces
{
    public interface ITenantIdentifiable
    {
        Guid TenantId { get; set; }
    }
}
