namespace OnlineStore.Application.Api.Repositories.Checkout;

using OnlineStore.Domain.Model.Checkout;

public interface ICartCommandRepository
{
    Task CreateAsync(Cart cart);
    Task UpdateAsync(Cart cart);
    Task DeleteAsync(Cart cart);
    Task DeleteByIdAsync(string id);
}
