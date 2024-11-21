namespace OnlineStore.Application.Services.Products;

using OnlineStore.Application.Query.Common;
using OnlineStore.Application.Query.Products;
using OnlineStore.Domain.Model.Products;

public interface IProductQueryService
{
    Task<Product> GetByIdAsync(int id, ProductInclude include = ProductInclude.None);
    Task<IListStructure<Product>> FindAllByQueryAsync(IProductQuery query);
}
