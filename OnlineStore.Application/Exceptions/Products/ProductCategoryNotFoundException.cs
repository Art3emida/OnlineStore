namespace OnlineStore.Application.Exceptions.Products;

using OnlineStore.Application.Exceptions.Common;

public class ProductCategoryNotFoundException : NotFoundException
{
    public ProductCategoryNotFoundException(string message) : base(message)
    {
    }
}
