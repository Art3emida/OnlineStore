namespace OnlineStore.Application.DtoFactory.Products;

using OnlineStore.Application.Command.Products;

public class UpdateProductCategoryCommandFactory : IUpdateProductCategoryCommandFactory
{
    public UpdateProductCategoryCommand Create(
        int id,
        string name,
        string description,
        string? photo = null
    ) {
        return new UpdateProductCategoryCommand(
            Id: id,
            Name: name,
            Description: description,
            Photo: photo
        );
    }
}
