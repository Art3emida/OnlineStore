namespace OnlineStore.Domain.Api.Products;

using OnlineStore.Domain.Model.Products;

public interface IProductPhoto
{
    public int Id { get; set; }
    public string Url { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}
