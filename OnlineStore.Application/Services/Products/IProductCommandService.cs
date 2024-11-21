namespace OnlineStore.Application.Services.Products;

using OnlineStore.Application.Command.Products;
using OnlineStore.Domain.Model.Products;

public interface IProductCommandService
{
    Task CreateAsync(CreateProductCommand command);
    Task UpdateAsync(UpdateProductCommand command);
    Task UpdateAsync(Product product);
    Task DeleteByIdAsync(int id);
}
