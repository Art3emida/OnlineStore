namespace OnlineStoreWeb.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Command.Products;
using OnlineStore.Application.Dto.Products;
using OnlineStore.Application.DtoFactory.Products;
using OnlineStore.Application.Exceptions.Common;
using OnlineStore.Application.Query.Common;
using OnlineStore.Application.Query.Products;
using OnlineStore.Application.Services.Products;
using OnlineStore.Domain.Model.Products;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductsController : Controller
{
    [Route("admin/products")]
    public async Task<IActionResult> Index(
        [FromServices] IProductQueryFactory queryFactory,
        [FromServices] IProductQueryService queryService,
        int page = 1
    ) {
        IProductQuery query = queryFactory.Create(
            limit: 10,
            page: page,
            include: ProductInclude.Photos
        );
        IListStructure<Product> listStructure = await queryService.FindAllByQueryAsync(query);

        return View(listStructure);
    }

    [Route("admin/products/create")]
    public async Task<IActionResult> Create(
        [FromServices] IProductCategoryQueryFactory queryFactory,
        [FromServices] IProductCategoryQueryService queryService
    ) {
        ViewBag.Categories = await GetCategoriesList(queryFactory, queryService);

        return View();
    }

    [HttpPost]
    [Route("admin/products/create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateProductRequestDto requestDto,
        [FromServices] ICreateProductCommandFactory commandFactory,
        [FromServices] IProductCommandService commandService,
        [FromServices] IProductCategoryQueryFactory queryFactory,
        [FromServices] IProductCategoryQueryService queryService
    ) {
        if (ModelState.IsValid)
        {
            CreateProductCommand command = commandFactory.Create(
                name: requestDto.Name,
                description: requestDto.Description,
                price: requestDto.Price,
                categoryId: requestDto.CategoryId,
                photos: requestDto.Photos
            );

            try
            {
                await commandService.CreateAsync(command);
                return RedirectToAction(nameof(Index));
            }
            catch (ValidationException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
        }

        ViewBag.Categories = await GetCategoriesList(queryFactory, queryService);

        return View(requestDto);
    }

    [Route("admin/products/edit/{id}")]
    public async Task<IActionResult> Edit(
        int id,
        [FromServices] IProductQueryService queryService,
        [FromServices] IProductCategoryQueryFactory categoryQueryFactory,
        [FromServices] IProductCategoryQueryService categoryQueryService
    ) {
        Product product = await queryService.GetByIdAsync(
            id,
            include: ProductInclude.Photos
        );
        ViewBag.Categories = await GetCategoriesList(categoryQueryFactory, categoryQueryService);

        return View(new UpdateProductRequestDto(
            Id: product.Id,
            Name: product.Name,
            Description: product.Description,
            Price: product.Price,
            CategoryId: product.CategoryId,
            Photos: product.Photos?.Select(p => p.PhotoUrl).ToList()
        ));
    }

    [HttpPost]
    [Route("admin/products/edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        UpdateProductRequestDto requestDto,
        [FromServices] IUpdateProductCommandFactory commandFactory,
        [FromServices] IProductCommandService commandService,
        [FromServices] IProductCategoryQueryFactory queryFactory,
        [FromServices] IProductCategoryQueryService queryService
    ) {
        if (ModelState.IsValid)
        {
            UpdateProductCommand command = commandFactory.Create(
                id: requestDto.Id,
                name: requestDto.Name,
                description: requestDto.Description,
                price: requestDto.Price,
                categoryId: requestDto.CategoryId,
                photos: requestDto.Photos
            );

            try
            {
                await commandService.UpdateAsync(command);
                return RedirectToAction(nameof(Index));
            }
            catch (ValidationException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
        }

        ViewBag.Categories = await GetCategoriesList(queryFactory, queryService);

        return View(requestDto);
    }

    [Route("admin/products/delete/{id}")]
    public async Task<IActionResult> Delete(
        int id,
        [FromServices] IProductQueryService queryService
    ) {
        Product product = await queryService.GetByIdAsync(id);

        return View(product);
    }

    [HttpPost]
    [Route("admin/products/delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(
        int id,
        [FromServices] IProductCommandService commandService
    ) {
        await commandService.DeleteByIdAsync(id);

        return RedirectToAction(nameof(Index));
    }

    private async Task<IEnumerable<ProductCategory>> GetCategoriesList(
        IProductCategoryQueryFactory queryFactory,
        IProductCategoryQueryService queryService
    ) {
        IProductCategoryQuery categoryQuery = queryFactory.Create(withoutPagination: true);
        IListStructure<ProductCategory> listStructure = await queryService.FindAllByQueryAsync(categoryQuery);

        return listStructure.Items;
    }
}
