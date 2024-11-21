namespace OnlineStore.Domain.Api.Products;

using OnlineStore.Domain.Model.Products;

public interface IProduct
{
    int Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    decimal Price { get; set; }
    int CategoryId { get; set; }
    ProductCategory Category { get; set; }
    ICollection<ProductPhoto> Photos { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
}
