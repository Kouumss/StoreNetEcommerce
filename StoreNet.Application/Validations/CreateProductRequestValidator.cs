//using FluentValidation;
//using StoreNet.Domain.Entities;

//namespace StoreNet.Application.Validations;

//public class CreateProductRequestValidator : AbstractValidator<Product>
//{
//    public CreateProductRequestValidator()
//    {
//        RuleFor(x => x.Name)
//            .NotEmpty().WithMessage("Product name is required.")
//            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
//        RuleFor(x => x.Description)
//            .NotEmpty().WithMessage("Product description is required.")
//            .MaximumLength(500).WithMessage("Product description must not exceed 500 characters.");
//        RuleFor(x => x.Price)
//            .GreaterThan(0).WithMessage("Product price must be greater than zero.")
//            .LessThanOrEqualTo(10000).WithMessage("Product price must not exceed 10000.");
//        RuleFor(x => x.StockQuantity)
//            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be at least 0.")
//            .LessThanOrEqualTo(1000).WithMessage("Stock quantity must not exceed 1000.");
//        RuleFor(x => x.CategoryId)
//            .NotEmpty().WithMessage("Category ID is required.");
//        RuleFor(x => x.BrandId)
//            .NotEmpty().WithMessage("Brand ID is required.");
//        RuleFor(x => x.ImageUrl)
//            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute)).WithMessage("Image URL must be a valid URL.");
//        RuleFor(x => x.DiscountPercent)
//            .GreaterThanOrEqualTo(0).WithMessage("Discount percent must be at least 0.")
//            .LessThanOrEqualTo(100).WithMessage("Discount percent must not exceed 100.");
//    }
//}
