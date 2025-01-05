namespace Shared.Kernel.DomainKernel.Interfaces
{
    public interface IAuditable
    {
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset LastUpdatedAt { get; set; }
    }
}
