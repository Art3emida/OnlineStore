namespace OnlineStore.Application.DtoFactory.Products;

using OnlineStore.Application.Command.Products;

public class CreateProductCategoryCommandFactory : ICreateProductCategoryCommandFactory
{
    public CreateProductCategoryCommand Create(
        string name,
        string description,
        string? photo = null
    ) {
        return new CreateProductCategoryCommand(
            Name: name,
            Description: description,
            Photo: photo
        );
    }
}
