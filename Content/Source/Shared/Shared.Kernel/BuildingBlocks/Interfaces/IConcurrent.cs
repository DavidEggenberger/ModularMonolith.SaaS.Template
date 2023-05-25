namespace Shared.Features.BuildingBlocks.Interfaces
{
    public interface IConcurrent
    {
        byte[] RowVersion { get; set; }
    }
}
