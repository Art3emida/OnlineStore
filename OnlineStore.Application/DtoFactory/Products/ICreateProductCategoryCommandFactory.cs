namespace OnlineStore.Application.DtoFactory.Products;

using OnlineStore.Application.Command.Products;

public interface ICreateProductCategoryCommandFactory
{
    CreateProductCategoryCommand Create(
        string name,
        string description,
        string? photo = null
    );
}
