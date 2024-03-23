namespace Shared.Kernel.BuildingBlocks.Services.ModelValidation
{
    public class ValidationServiceResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; }
    }
}
