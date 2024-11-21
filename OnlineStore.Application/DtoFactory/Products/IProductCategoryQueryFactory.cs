namespace OnlineStore.Application.DtoFactory.Products;

using OnlineStore.Application.ConstantBag.Products;
using OnlineStore.Application.Query.Products;

public interface IProductCategoryQueryFactory
{
    IProductCategoryQuery Create(
        int? limit = ProductCategoryPaginationBag.PerPageDefault,
        int? offset = null,
        int? page = null,
        bool withoutPagination = false,
        ProductCategorySort sort = ProductCategorySort.NameAsc,
        ProductCategoryInclude include = ProductCategoryInclude.None
    );
}
