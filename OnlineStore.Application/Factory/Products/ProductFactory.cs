namespace OnlineStore.Application.Factory.Products;

using OnlineStore.Application.Command.Products;
using OnlineStore.Domain.Model.Products;

public class ProductFactory : IProductFactory
{
    public Product CreateFromCommand(CreateProductCommand command)
    {
        return new Product {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            CategoryId = command.CategoryId,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
