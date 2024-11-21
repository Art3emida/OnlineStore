namespace OnlineStore.Application.Query.Products;

using OnlineStore.Application.Query.Common;

public record ProductCategoryQuery(
    IPaginationControl? PaginationControl,
    ProductCategorySort Sort,
    ProductCategoryInclude Include
) : IProductCategoryQuery;
