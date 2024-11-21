using OnlineStore.Domain.Api.Products;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Domain.Model.Products;

[Index(nameof(CreatedAt))]
public class Product : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public ProductCategory Category { get; set; }
    public ICollection<ProductPhoto> Photos { get; set; } = new List<ProductPhoto>();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
