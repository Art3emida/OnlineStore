namespace OnlineStore.Application.Query.Products;

using OnlineStore.Application.Query.Common;

public record ProductQuery(
    int? CategoryId,
    IPaginationControl? PaginationControl,
    ProductSort Sort,
    ProductInclude Include
) : IProductQuery;
