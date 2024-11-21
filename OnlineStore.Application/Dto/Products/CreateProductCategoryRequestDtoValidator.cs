namespace OnlineStore.Application.Dto.Products;

using FluentValidation;

public class CreateProductCategoryRequestDtoValidator : AbstractValidator<CreateProductCategoryRequestDto>
{
    public CreateProductCategoryRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Category description is required.")
            .MaximumLength(500).WithMessage("Category description must not exceed 500 characters.");
    }
}
