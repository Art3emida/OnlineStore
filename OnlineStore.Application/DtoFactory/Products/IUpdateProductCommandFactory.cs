namespace OnlineStore.Application.DtoFactory.Products;

using OnlineStore.Application.Command.Products;

public interface IUpdateProductCommandFactory
{
    UpdateProductCommand Create(
        int id,
        string name,
        string description,
        decimal price,
        int categoryId,
        List<string>? photos = null
    );
}
