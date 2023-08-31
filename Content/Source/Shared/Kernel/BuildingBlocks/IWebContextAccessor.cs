namespace Shared.Kernel.BuildingBlocks
{
    public interface IWebContextAccessor
    {
        public Uri BaseURI { get; set; }
    }
}
