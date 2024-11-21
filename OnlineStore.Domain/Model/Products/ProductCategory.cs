namespace OnlineStore.Domain.Model.Products;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Api.Products;

[Index(nameof(Name))]
public class ProductCategory : IProductCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Photo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
