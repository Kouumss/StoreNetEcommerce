﻿//using FluentValidation;
//using StoreNet.Domain.Dtos.Authentication;

//namespace StoreNet.Application.Validations;
//public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
//{
//    public LoginUserRequestValidator()
//    {
//        RuleFor(x => x.Email)
//            .NotEmpty().WithMessage("Email is required.")
//            .EmailAddress().WithMessage("Invalid email format.");

//        RuleFor(x => x.Password)
//            .NotEmpty().WithMessage("Password is required.");
//    }
//}
