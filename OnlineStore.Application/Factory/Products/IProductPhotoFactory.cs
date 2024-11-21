namespace OnlineStore.Application.Factory.Products;

using OnlineStore.Domain.Model.Products;

public interface IProductPhotoFactory
{
    ProductPhoto Create(
        int productId,
        string photoUrl
    );
}
