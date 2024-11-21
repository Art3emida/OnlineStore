namespace OnlineStore.Application.DtoFactory.Products;

using OnlineStore.Application.Command.Products;

public interface IUpdateProductCategoryCommandFactory
{
    UpdateProductCategoryCommand Create(
        int id,
        string name,
        string description,
        string? photo = null
    );
}
