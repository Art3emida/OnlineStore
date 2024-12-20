namespace OnlineStore.Application.Dto.Products;

using FluentValidation;

public class UpdateProductCategoryRequestDtoValidator : AbstractValidator<UpdateProductCategoryRequestDto>
{
    public UpdateProductCategoryRequestDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Category id is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Category description is required.")
            .MaximumLength(500).WithMessage("Category description must not exceed 500 characters.");
    }
}
