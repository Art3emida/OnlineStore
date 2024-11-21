namespace OnlineStore.Application.Dto.Users;

using FluentValidation;

public class LoginUserRequestDtoValidator : AbstractValidator<LoginUserRequestDto>
{
    public LoginUserRequestDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.RememberMe)
            .NotNull().WithMessage("Remember me selection is required.");
    }
}
