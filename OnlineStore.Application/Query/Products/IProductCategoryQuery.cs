namespace OnlineStore.Application.Query.Products;

using OnlineStore.Application.Dto.Common;
using OnlineStore.Application.Query.Common;

public interface IProductCategoryQuery : IDto
{
    IPaginationControl? PaginationControl { get; }
    ProductCategorySort Sort { get; }
    ProductCategoryInclude Include { get; }
}
