namespace OnlineStoreWeb.Areas.Shop.Controllers;

using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DtoFactory.Products;
using OnlineStore.Application.Query.Common;
using OnlineStore.Application.Query.Products;
using OnlineStore.Application.Services.Products;
using OnlineStore.Domain.Model.Products;
using OnlineStoreWeb.Areas.Shop.ViewModels;

[Area("shop")]
public class ShopController : Controller
{
    [Route("shop")]
    public async Task<IActionResult> Index(
        [FromServices] IProductQueryFactory queryFactory,
        [FromServices] IProductQueryService queryService,
        int page = 1
    ) {
        IProductQuery query = queryFactory.Create(
            limit: 9,
            page: page,
            include: ProductInclude.Photos
        );
        IListStructure<Product> listStructure = await queryService.FindAllByQueryAsync(query);

        return View(listStructure);
    }

    [HttpPost]
    [Route("shop")]
    public async Task<IActionResult> IndexMore(
        int offset,
        int limit,
        [FromServices] IProductQueryFactory queryFactory,
        [FromServices] IProductQueryService queryService
    ) {
        IProductQuery query = queryFactory.Create(
            limit: limit,
            offset: offset,
            include: ProductInclude.Photos
        );
        IListStructure<Product> listStructure = await queryService.FindAllByQueryAsync(query);

        return PartialView("_ProductList", listStructure.Items);
    }

    [Route("shop/catalog")]
    public async Task<IActionResult> Catalog(
        [FromServices] IProductCategoryQueryFactory queryFactory,
        [FromServices] IProductCategoryQueryService queryService
    ) {
        IProductCategoryQuery query = queryFactory.Create(
            withoutPagination: true
        );
        IListStructure<ProductCategory> listStructure = await queryService.FindAllByQueryAsync(query);
        
        return View(listStructure.Items);
    }

    [Route("shop/category/{id}")]
    public async Task<IActionResult> Category(
        [FromServices] IProductCategoryQueryService categoryQueryService,
        [FromServices] IProductQueryFactory productQueryFactory,
        [FromServices] IProductQueryService productQueryService,
        int id,
        int page = 1
    ) {
        ProductCategory category = await categoryQueryService.GetByIdAsync(id);
        IProductQuery query = productQueryFactory.Create(
            categoryId: id,
            limit: 9,
            page: page,
            include: ProductInclude.Photos
        );
        IListStructure<Product> listStructure = await productQueryService.FindAllByQueryAsync(query);

        return View(new CategoryViewModel{
            Category = category,
            ProductsList = listStructure
        });
    }

    [HttpPost]
    [Route("shop/category/{id}")]
    public async Task<IActionResult> CategoryMore(
        int id,
        int offset,
        int limit,
        [FromServices] IProductQueryFactory queryFactory,
        [FromServices] IProductQueryService queryService
    ) {
        IProductQuery query = queryFactory.Create(
            categoryId: id,
            limit: limit,
            offset: offset,
            include: ProductInclude.Photos
        );
        IListStructure<Product> listStructure = await queryService.FindAllByQueryAsync(query);

        return PartialView("_ProductList", listStructure.Items);
    }

    [Route("shop/product/{id}")]
    public async Task<IActionResult> Details(
        int id,
        [FromServices] IProductQueryService queryService
    ) {
        Product product = await queryService.GetByIdAsync(
            id,
            include: ProductInclude.Photos
        );
        
        return View(product);
    }
}
