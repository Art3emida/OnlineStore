namespace OnlineStore.Application.Services.Checkout;

using OnlineStore.Application.Api.Repositories.Checkout;
using OnlineStore.Application.Factory.Checkout;
using OnlineStore.Domain.Model.Checkout;

public class CartCommandService : ICartCommandService
{
    private readonly ICartCommandRepository _commandRepository;
    private readonly ICartFactory _cartFactory;
    private readonly ICartItemFactory _cartItemFactory;

    public CartCommandService(
        ICartCommandRepository commandRepository,
        ICartFactory cartFactory,
        ICartItemFactory cartItemFactory
    )
    {
        _commandRepository = commandRepository;
        _cartFactory = cartFactory;
        _cartItemFactory = cartItemFactory;
    }

    public async Task CreateAsync(int userId, string? cartId = null)
    {
        Cart cart = _cartFactory.Create(userId, cartId);
        await _commandRepository.CreateAsync(cart);
    }

    public async Task UpdateAsync(Cart cart)
    {
        await _commandRepository.UpdateAsync(cart);
    }
}
