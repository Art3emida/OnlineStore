namespace OnlineStore.Application.Factory.Checkout;

using OnlineStore.Application.Command.Checkout;
using OnlineStore.Domain.Model.Checkout;

public interface ICartItemFactory
{
    CartItem CreateFromCommand(CreateCartItemCommand command);
}
