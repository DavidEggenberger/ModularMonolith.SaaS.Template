namespace Shared.Kernel.Interfaces
{
    public interface IConcurrent
    {
        byte[] RowVersion { get; set; }
    }
}
