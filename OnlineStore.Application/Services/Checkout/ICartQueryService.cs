namespace OnlineStore.Application.Services.Checkout;

using OnlineStore.Domain.Model.Checkout;

public interface ICartQueryService
{
    Task<Cart> GetByIdAsync(string id);
    Task<Cart> GetByUserIdAsync(int userId);
}
