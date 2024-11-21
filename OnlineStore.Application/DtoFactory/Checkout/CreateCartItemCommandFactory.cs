namespace OnlineStore.Application.DtoFactory.Checkout;

using OnlineStore.Application.Command.Checkout;

public class CreateCartItemCommandFactory : ICreateCartItemCommandFactory
{
    public CreateCartItemCommand Create(
        string cartId,
        int productId,
        string name,
        int quantity,
        decimal price
    ) {
        return new CreateCartItemCommand(
            CartId: cartId,
            ProductId: productId,
            Name: name,
            Quantity: quantity,
            Price: price
        );
    }
}
