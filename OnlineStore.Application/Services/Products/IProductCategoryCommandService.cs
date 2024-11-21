namespace OnlineStore.Application.Services.Products;

using OnlineStore.Application.Command.Products;
using OnlineStore.Domain.Model.Products;

public interface IProductCategoryCommandService
{
    Task CreateAsync(CreateProductCategoryCommand command);
    Task UpdateAsync(UpdateProductCategoryCommand command);
    Task UpdateAsync(ProductCategory category);
    Task DeleteByIdAsync(int id);
}
