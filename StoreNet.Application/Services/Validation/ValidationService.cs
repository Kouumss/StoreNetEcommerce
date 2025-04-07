using FluentValidation;
using StoreNet.Application.Interfaces.Validation;

namespace StoreNet.Application.Services.Validation;

public class ValidationService : IValidationService
{
    public async Task<ServiceResult> ValidateAsync<T>(T model, IValidator<T> validator)
    {
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

            return ServiceResult.Failure(string.Join(", ", errors));
        }
        return ServiceResult.Success("Validation succeeded");
    }
}