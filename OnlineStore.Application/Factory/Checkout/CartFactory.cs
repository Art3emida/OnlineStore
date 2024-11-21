namespace OnlineStore.Application.Factory.Checkout;

using OnlineStore.Domain.Model.Checkout;

public class CartFactory : ICartFactory
{
    public Cart Create(int userId, string? cartId)
    {
        return new Cart {
            Id = cartId ?? Guid.NewGuid().ToString(),
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
