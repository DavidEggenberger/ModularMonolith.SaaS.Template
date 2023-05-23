namespace Shared.Kernel.Interfaces
{
    public interface ITenantIdentifiable
    {
        Guid TenantId { get; set; }
    }
}
