using FluentValidation;

namespace StoreNet.Application.Interfaces.Validation;

public interface IValidationService
{
    Task<ServiceResult> ValidateAsync<T>(T model, IValidator<T> validator);
}
