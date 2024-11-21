namespace OnlineStore.Application.Api.Repositories.Products;

using OnlineStore.Domain.Model.Products;

public interface IProductCommandRepository
{
    Task CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Product product);
    Task DeleteByIdAsync(int id);
}
