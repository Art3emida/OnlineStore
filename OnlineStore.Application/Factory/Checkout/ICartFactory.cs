namespace OnlineStore.Application.Factory.Checkout;

using OnlineStore.Domain.Model.Checkout;

public interface ICartFactory
{
    Cart Create(int userId, string? cartId);
}
