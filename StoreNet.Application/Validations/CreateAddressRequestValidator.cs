using FluentValidation;
using StoreNet.Domain.Entities;

namespace StoreNet.Application.Validations;

public class CreateAddressRequestValidator : AbstractValidator<Address>
{
    public CreateAddressRequestValidator()
    {
       RuleFor(x => x.StreetName)
            .NotEmpty()
            .WithMessage("Street is required.")
            .Length(1, 100)
            .WithMessage("Street must be between 1 and 100 characters.");
        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City is required.")
            .Length(1, 50)
            .WithMessage("City must be between 1 and 50 characters.");
        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("State is required.")
            .Length(1, 50)
            .WithMessage("State must be between 1 and 50 characters.");
        RuleFor(x => x.PostalCode)
            .NotEmpty()
            .WithMessage("ZipCode is required.")
            .Matches(@"^\d{5}(-\d{4})?$")
            .WithMessage("ZipCode must be a valid format (e.g., 12345 or 12345-6789).");
        RuleFor(x => x.Country)
            .NotEmpty()
            .WithMessage("Country is required.")
            .Length(1, 50)
            .WithMessage("Country must be between 1 and 50 characters.");
    }
}