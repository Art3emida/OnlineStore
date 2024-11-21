namespace OnlineStore.Application.Api.Repositories.Products;

using OnlineStore.Application.Query.Common;
using OnlineStore.Application.Query.Products;
using OnlineStore.Domain.Model.Products;

public interface IProductCategoryQueryRepository
{
    Task<ProductCategory?> GetByIdAsync(int id, ProductCategoryInclude include = ProductCategoryInclude.None);
    Task<IListStructure<ProductCategory>> FindAllByQueryAsync(IProductCategoryQuery query);
}
