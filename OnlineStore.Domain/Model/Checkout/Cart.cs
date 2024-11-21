namespace OnlineStore.Domain.Model.Checkout;

using Microsoft.EntityFrameworkCore;

[Index(nameof(UserId))]
public class Cart
{
    public string Id { get; set; }
    public int UserId { get; set; }
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
