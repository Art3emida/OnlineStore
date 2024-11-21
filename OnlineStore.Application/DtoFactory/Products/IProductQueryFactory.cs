namespace OnlineStore.Application.DtoFactory.Products;

using OnlineStore.Application.ConstantBag.Products;
using OnlineStore.Application.Query.Products;

public interface IProductQueryFactory
{
    IProductQuery Create(
        int? categoryId = null,
        int? limit = ProductPaginationBag.PerPageDefault,
        int? offset = null,
        int? page = null,
        bool withoutPagination = false,
        ProductSort sort = ProductSort.CreatedAtDesc,
        ProductInclude include = ProductInclude.None
    );
}
