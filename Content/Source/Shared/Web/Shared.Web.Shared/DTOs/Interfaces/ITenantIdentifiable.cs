namespace Shared.Web.Shared.DTOs.Interfaces
{
    public interface ITenantIdentifiable
    {
        Guid TenantId { get; set; }
    }
}
