﻿//using FluentValidation;
//using StoreNet.Application.Dtos.User;

//namespace StoreNet.Application.Validations;

//public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
//{
//    public CreateUserRequestValidator()
//    {
//        RuleFor(x => x.Email)
//            .NotEmpty()
//            .EmailAddress();
//        RuleFor(x => x.Password)
//            .NotEmpty().WithMessage("Password is required.")
//            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
//            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
//            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
//            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
//            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

//        RuleFor(x => x.ConfirmPassword)
//            .Equal(x => x.Password).WithMessage("Passwords do not match.");
//    }
//}
