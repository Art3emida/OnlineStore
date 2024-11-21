namespace OnlineStore.Application.Exceptions.Products;

using OnlineStore.Application.Exceptions.Common;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(string message) : base(message)
    {
    }
}
