namespace OnlineStore.Domain.Model.Checkout;

using System.ComponentModel.DataAnnotations.Schema;
using OnlineStore.Domain.Model.Products;

public class CartItem
{
    public Guid Id { get; set; }
    public string CartId { get; set; }
    public Cart Cart { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
}
