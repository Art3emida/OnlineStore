namespace OnlineStore.Application.Factory.Products;

using OnlineStore.Application.Command.Products;
using OnlineStore.Domain.Model.Products;

public class ProductCategoryFactory : IProductCategoryFactory
{
    public ProductCategory CreateFromCommand(CreateProductCategoryCommand command)
    {
        return new ProductCategory {
            Name = command.Name,
            Description = command.Description,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
