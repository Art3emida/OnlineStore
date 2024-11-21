namespace OnlineStore.Application.Dto.Users;

using FluentValidation;

public class RegisterUserRequestDtoValidator : AbstractValidator<RegisterUserRequestDto>
{
    public RegisterUserRequestDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
            .Equal(x => x.Password).WithMessage("The password and confirmation password do not match.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[0-9\s\-]*$").WithMessage("Phone number must be valid.");

        RuleFor(x => x.StreetAddress)
            .MaximumLength(200).WithMessage("Street address cannot exceed 200 characters.");

        RuleFor(x => x.City)
            .MaximumLength(100).WithMessage("City cannot exceed 100 characters.");

        RuleFor(x => x.State)
            .MaximumLength(100).WithMessage("State cannot exceed 100 characters.");

        RuleFor(x => x.PostalCode)
            .Matches(@"^\d{5}(-\d{4})?$").WithMessage("Postal code must be a valid format.");
    }
}
