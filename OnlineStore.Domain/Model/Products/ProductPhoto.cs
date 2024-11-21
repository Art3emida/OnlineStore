namespace OnlineStore.Domain.Model.Products;

public class ProductPhoto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public string PhotoUrl { get; set; }
}
