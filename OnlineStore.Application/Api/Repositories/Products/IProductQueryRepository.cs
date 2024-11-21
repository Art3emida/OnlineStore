namespace OnlineStore.Application.Api.Repositories.Products;

using OnlineStore.Application.Query.Common;
using OnlineStore.Application.Query.Products;
using OnlineStore.Domain.Model.Products;

public interface IProductQueryRepository
{
    Task<Product?> GetByIdAsync(int id, ProductInclude include = ProductInclude.None);
    Task<IListStructure<Product>> FindAllByQueryAsync(IProductQuery query);
}
