namespace OnlineStore.Application.DtoFactory.Products;

using OnlineStore.Application.ConstantBag.Products;
using OnlineStore.Application.DtoFactory.Common;
using OnlineStore.Application.Query.Common;
using OnlineStore.Application.Query.Products;

public class ProductCategoryQueryFactory : IProductCategoryQueryFactory
{
    private readonly IPaginationControlFactory _productPaginationControlFactory;

    public ProductCategoryQueryFactory(IPaginationControlFactory productPaginationControlFactory)
    {
        _productPaginationControlFactory = productPaginationControlFactory;
    }

    public IProductCategoryQuery Create(
        int? limit = ProductCategoryPaginationBag.PerPageDefault,
        int? offset = null,
        int? page = null,
        bool withoutPagination = false,
        ProductCategorySort sort = ProductCategorySort.NameAsc,
        ProductCategoryInclude include = ProductCategoryInclude.None
    ) {
        if (page.HasValue && limit.HasValue && !offset.HasValue)
        {
            offset = (page - 1) * limit;
        }

        IPaginationControl? paginationControl = withoutPagination ? null : _productPaginationControlFactory.Create(
            limit: limit ?? ProductPaginationBag.PerPageDefault,
            offset: offset
        );
        
        return new ProductCategoryQuery(
            PaginationControl: paginationControl,
            Sort: sort,
            Include: include
        );
    }
}
