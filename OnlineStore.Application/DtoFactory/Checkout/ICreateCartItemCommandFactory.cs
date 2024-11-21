namespace OnlineStore.Application.DtoFactory.Checkout;

using OnlineStore.Application.Command.Checkout;

public interface ICreateCartItemCommandFactory
{
    CreateCartItemCommand Create(
        string cartId,
        int productId,
        string name,
        int quantity,
        decimal price
    );
}
