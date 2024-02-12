namespace Shared.Kernel.Interfaces
{
    public interface IAuditable
    {
        Guid CreatedByUserId { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset LastUpdatedAt { get; set; }
    }
}
