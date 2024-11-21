namespace OnlineStore.Application.Query.Products;

using OnlineStore.Application.Dto.Common;
using OnlineStore.Application.Query.Common;

public interface IProductQuery : IDto
{
    int? CategoryId { get; }
    IPaginationControl? PaginationControl { get; }
    ProductSort Sort { get; }
    ProductInclude Include { get; }
}
