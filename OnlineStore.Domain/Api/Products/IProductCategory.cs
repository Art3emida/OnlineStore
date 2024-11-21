namespace OnlineStore.Domain.Api.Products;

using OnlineStore.Domain.Model.Products;

public interface IProductCategory
{
    int Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
    ICollection<Product> Products { get; set; }
}
