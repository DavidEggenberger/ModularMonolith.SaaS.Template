using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using static FluentValidation.AssemblyScanner;

namespace Shared.Kernel.BuildingBlocks.Services.ModelValidation
{
    public class ValidationService : IValidationService
    {
        private readonly List<AssemblyScanResult> assemblyScanResult = new List<AssemblyScanResult>();
        private readonly IServiceProvider serviceProvider;

        public ValidationService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ValidationServiceResult Validate<T>(T model)
        {
            var validator = GetValidatorForModel(model, typeof(T));
            if (validator == null)
            {
                //throw new ValidationServiceException("No Validator is registerd");
            }
            ValidationResult validationResult = validator.Validate(new ValidationContext<T>(model));
            return new ValidationServiceResult
            {
                IsValid = validationResult.IsValid,
                Errors = validationResult.Errors.Select(s => s.ErrorMessage).ToList()
            };
        }

        public void ThrowIfInvalidModel<T>(T model)
        {
            var validator = GetValidatorForModel(model, typeof(T));
            if (validator == null)
            {
                //throw new ValidationServiceException("No Validator is registerd");
            }
            ValidationResult validationResult = validator.Validate(new ValidationContext<T>(model));
            if (validationResult.IsValid is false)
            {
                throw new Exception();
            }
        }

        private IValidator GetValidatorForModel(object model, Type modelType)
        {
            assemblyScanResult.AddRange(FindValidatorsInAssembly(modelType.Assembly));

            var interfaceValidatorType = typeof(IValidator<>).MakeGenericType(model.GetType());

            Type modelValidatorType = assemblyScanResult.FirstOrDefault(i => interfaceValidatorType.IsAssignableFrom(i.InterfaceType))?.ValidatorType;

            if (modelValidatorType == null)
            {
                return null;
            }

            return (IValidator)ActivatorUtilities.CreateInstance(serviceProvider, modelValidatorType);
        }
    }
}
