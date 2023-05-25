namespace Shared.Features.DomainKernel.Interfaces
{
    public interface IConcurrent
    {
        byte[] RowVersion { get; set; }
    }
}
