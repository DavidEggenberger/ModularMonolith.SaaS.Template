namespace Shared.Kernel.BuildingBlocks.ModelValidation
{
    public interface IValidationService
    {
        void ThrowIfInvalidModel<T>(T model);
        ValidationServiceResult Validate<T>(T model);
    }
}