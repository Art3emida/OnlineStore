namespace OnlineStore.Application.DtoFactory.Products;

using OnlineStore.Application.Command.Products;

public class CreateProductCommandFactory : ICreateProductCommandFactory
{
    public CreateProductCommand Create(
        string name,
        string description,
        decimal price,
        int categoryId,
        List<string>? photos = null
    ) {
        return new CreateProductCommand(
            Name: name,
            Description: description,
            Price: price,
            CategoryId: categoryId,
            Photos: photos
        );
    }
}
