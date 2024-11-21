namespace OnlineStore.Application.Services.Products;

using OnlineStore.Application.Query.Common;
using OnlineStore.Application.Query.Products;
using OnlineStore.Domain.Model.Products;

public interface IProductCategoryQueryService
{
    Task<ProductCategory> GetByIdAsync(int id, ProductCategoryInclude include = ProductCategoryInclude.None);
    Task<IListStructure<ProductCategory>> FindAllByQueryAsync(IProductCategoryQuery query);
}
