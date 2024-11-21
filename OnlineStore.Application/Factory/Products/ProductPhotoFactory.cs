namespace OnlineStore.Application.Factory.Products;

using OnlineStore.Domain.Model.Products;

public class ProductPhotoFactory : IProductPhotoFactory
{
    public ProductPhoto Create(
        int productId,
        string photoUrl
    ) {
        return new ProductPhoto {
            ProductId = productId,
            PhotoUrl = photoUrl,
        };
    }
}
