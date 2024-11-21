namespace OnlineStore.Infrastructure.Repositories.Products;

using OnlineStore.Application.Api.Repositories.Products;
using OnlineStore.Domain.Model.Products;
using OnlineStore.Infrastructure.Persistence.Context;
using OnlineStore.Infrastructure.Repositories.Common;

public class ProductCategoryCommandRepository : CommandRepository<ProductCategory, int>, IProductCategoryCommandRepository
{
    public ProductCategoryCommandRepository(MasterDbContext dbContext) : base(dbContext) {}

    public new async Task CreateAsync(ProductCategory category)
    {
        await base.CreateAsync(category);
    }

    public new async Task UpdateAsync(ProductCategory category)
    {
        await base.UpdateAsync(category);
    }

    public new async Task DeleteAsync(ProductCategory category)
    {
        await base.DeleteAsync(category);
    }

    public new async Task DeleteByIdAsync(int id)
    {
        await base.DeleteByIdAsync(id);
    }
}
