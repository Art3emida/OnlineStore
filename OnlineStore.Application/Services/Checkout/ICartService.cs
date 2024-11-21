namespace OnlineStore.Application.Services.Checkout;

using OnlineStore.Domain.Model.Checkout;

public interface ICartService
{
    Task AddToCartAsync(int productId, int quantity, int? userId = null);
    Task RemoveFromCartAsync(int productId, int? userId = null);
    Task UpdateQuantityAsync(int productId, int quantity, int? userId = null);
    Task<int> GetCartItemCountAsync(int? userId = null);
    Task<Cart> GetCartAsync(int? userId = null);
}
