namespace OnlineStore.Application.Factory.Products;

using OnlineStore.Application.Command.Products;
using OnlineStore.Application.Factory.Common;
using OnlineStore.Domain.Model.Products;

public interface IProductCategoryFactory : IFactory
{
    ProductCategory CreateFromCommand(CreateProductCategoryCommand command);
}
