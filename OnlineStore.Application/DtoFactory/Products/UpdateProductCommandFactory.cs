namespace OnlineStore.Application.DtoFactory.Products;

using OnlineStore.Application.Command.Products;

public class UpdateProductCommandFactory : IUpdateProductCommandFactory
{
    public UpdateProductCommand Create(
        int id,
        string name,
        string description,
        decimal price,
        int categoryId,
        List<string>? photos = null
    ) {
        return new UpdateProductCommand(
            Id: id,
            Name: name,
            Description: description,
            Price: price,
            CategoryId: categoryId,
            Photos: photos
        );
    }
}
