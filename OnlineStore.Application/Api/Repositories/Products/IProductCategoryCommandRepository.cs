namespace OnlineStore.Application.Api.Repositories.Products;

using OnlineStore.Domain.Model.Products;

public interface IProductCategoryCommandRepository
{
    Task CreateAsync(ProductCategory category);
    Task UpdateAsync(ProductCategory category);
    Task DeleteAsync(ProductCategory category);
    Task DeleteByIdAsync(int id);
}
