namespace OnlineStore.Application.Exceptions.Checkout;

using OnlineStore.Application.Exceptions.Common;

public class CartNotFoundException : NotFoundException
{
    public CartNotFoundException(string message) : base(message)
    {
    }
}
