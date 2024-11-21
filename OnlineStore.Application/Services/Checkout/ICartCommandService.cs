namespace OnlineStore.Application.Services.Checkout;

using OnlineStore.Domain.Model.Checkout;

public interface ICartCommandService
{
    Task CreateAsync(int userId, string? cartId = null);
    Task UpdateAsync(Cart cart);
}
