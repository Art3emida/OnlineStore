namespace OnlineStore.Application.Factory.Products;

using OnlineStore.Application.Command.Products;
using OnlineStore.Application.Factory.Common;
using OnlineStore.Domain.Model.Products;

public interface IProductFactory : IFactory
{
    Product CreateFromCommand(CreateProductCommand command);
}
