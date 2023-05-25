namespace Shared.Features.BuildingBlocks.Interfaces
{
    public interface ITenantIdentifiable
    {
        Guid TenantId { get; set; }
    }
}
