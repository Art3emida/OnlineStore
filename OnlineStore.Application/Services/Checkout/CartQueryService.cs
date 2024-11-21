namespace OnlineStore.Application.Services.Checkout;

using OnlineStore.Application.Api.Repositories.Checkout;
using OnlineStore.Application.ConstantBag.Checkout;
using OnlineStore.Application.Exceptions.Checkout;
using OnlineStore.Domain.Model.Checkout;

public class CartQueryService : ICartQueryService
{
    private readonly ICartQueryRepository _queryRepository;

    public CartQueryService(
        ICartQueryRepository queryRepository
    ) {
        _queryRepository = queryRepository;
    }

    public async Task<Cart> GetByIdAsync(string id)
    {
        Cart? cart = await _queryRepository.GetByIdAsync(id);

        if (cart == null)
        {
            throw new CartNotFoundException(CartExceptionBag.CartNotFound);
        }

        return cart;
    }

    public async Task<Cart> GetByUserIdAsync(int userId)
    {
        Cart? cart = await _queryRepository.GetByUserIdAsync(userId);

        if (cart == null)
        {
            throw new CartNotFoundException(CartExceptionBag.CartNotFound);
        }

        return cart;
    }
}
