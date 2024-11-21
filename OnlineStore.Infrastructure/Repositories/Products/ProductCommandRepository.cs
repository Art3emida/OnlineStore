namespace OnlineStore.Infrastructure.Repositories.Products;

using OnlineStore.Application.Api.Repositories.Products;
using OnlineStore.Domain.Model.Products;
using OnlineStore.Infrastructure.Persistence.Context;
using OnlineStore.Infrastructure.Repositories.Common;

public class ProductCommandRepository : CommandRepository<Product, int>, IProductCommandRepository
{
    public ProductCommandRepository(MasterDbContext dbContext) : base(dbContext) {}

    public new async Task CreateAsync(Product product)
    {
        await base.CreateAsync(product);
    }

    public new async Task UpdateAsync(Product product)
    {
        await base.UpdateAsync(product);
    }

    public new async Task DeleteAsync(Product product)
    {
        await base.DeleteAsync(product);
    }

    public new async Task DeleteByIdAsync(int id)
    {
        await base.DeleteByIdAsync(id);
    }
}
