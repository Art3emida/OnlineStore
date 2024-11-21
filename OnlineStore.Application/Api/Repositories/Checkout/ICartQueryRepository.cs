namespace OnlineStore.Application.Api.Repositories.Checkout;

using OnlineStore.Domain.Model.Checkout;

public interface ICartQueryRepository
{
    Task<Cart?> GetByIdAsync(string id);
    Task<Cart?> GetByUserIdAsync(int userId);
}
