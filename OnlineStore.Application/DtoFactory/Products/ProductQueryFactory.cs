namespace OnlineStore.Application.DtoFactory.Products;

using OnlineStore.Application.ConstantBag.Products;
using OnlineStore.Application.DtoFactory.Common;
using OnlineStore.Application.Query.Common;
using OnlineStore.Application.Query.Products;

public class ProductQueryFactory : IProductQueryFactory
{
    private readonly IPaginationControlFactory _productPaginationControlFactory;

    public ProductQueryFactory(IPaginationControlFactory productPaginationControlFactory)
    {
        _productPaginationControlFactory = productPaginationControlFactory;
    }

    public IProductQuery Create(
        int? categoryId = null,
        int? limit = ProductPaginationBag.PerPageDefault,
        int? offset = null,
        int? page = null,
        bool withoutPagination = false,
        ProductSort sort = ProductSort.CreatedAtDesc,
        ProductInclude include = ProductInclude.None
    ) {
        if (page.HasValue && limit.HasValue && !offset.HasValue)
        {
            offset = (page - 1) * limit;
        }

        IPaginationControl? paginationControl = withoutPagination ? null : _productPaginationControlFactory.Create(
            limit: limit,
            offset: offset
        );

        return new ProductQuery(
            CategoryId: categoryId,
            Sort: sort,
            PaginationControl: paginationControl,
            Include: include
        );
    }
}
