namespace OnlineStoreWeb.Areas.Shop.ViewModels;

using OnlineStore.Application.Query.Common;
using OnlineStore.Domain.Model.Products;

public class CategoryViewModel
{
    public ProductCategory Category { get; set; }
    public IListStructure<Product> ProductsList { get; set; }
}
