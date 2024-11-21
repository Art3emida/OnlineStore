namespace OnlineStore.Application.Factory.Checkout;

using OnlineStore.Application.Command.Checkout;
using OnlineStore.Domain.Model.Checkout;

public class CartItemFactory : ICartItemFactory
{
    public CartItem CreateFromCommand(CreateCartItemCommand command)
    {
        return new CartItem {
            CartId = command.CartId,
            ProductId = command.ProductId,
            Name = command.Name,
            Quantity = command.Quantity,
            Price = command.Price,
        };
    }
}
