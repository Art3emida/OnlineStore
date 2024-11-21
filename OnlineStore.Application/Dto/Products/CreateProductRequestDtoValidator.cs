namespace OnlineStore.Application.Dto.Products;

using FluentValidation;

public class CreateProductRequestDtoValidator : AbstractValidator<CreateProductRequestDto>
{
    public CreateProductRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Product description is required.")
            .MaximumLength(5000).WithMessage("Product description must not exceed 5000 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Category ID must be valid.");

        /*RuleFor(x => x.Photos)
            .NotEmpty().WithMessage("Product photos is required.");*/
    }
}
