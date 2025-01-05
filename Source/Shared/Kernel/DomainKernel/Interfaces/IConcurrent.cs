namespace Shared.Kernel.DomainKernel.Interfaces
{
    public interface IConcurrent
    {
        byte[] RowVersion { get; set; }
    }
}
