namespace OnlineStore.Application.DtoFactory.Products;

using OnlineStore.Application.Command.Products;

public interface ICreateProductCommandFactory
{
    CreateProductCommand Create(
        string name,
        string description,
        decimal price,
        int categoryId,
        List<string>? photos = null
    );
}
