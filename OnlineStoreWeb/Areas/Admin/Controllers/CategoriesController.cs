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
public class CategoriesController : Controller
{
    [Route("admin/categories")]
    public async Task<IActionResult> Index(
        [FromServices] IProductCategoryQueryFactory queryFactory,
        [FromServices] IProductCategoryQueryService queryService,
        int page = 1
    ) {
        IProductCategoryQuery query = queryFactory.Create(
            limit: 10,
            page: page
        );
        IListStructure<ProductCategory> listStructure = await queryService.FindAllByQueryAsync(query);

        return View(listStructure);
    }

    [Route("admin/categories/create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Route("admin/categories/create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateProductCategoryRequestDto requestDto,
        [FromServices] ICreateProductCategoryCommandFactory commandFactory,
        [FromServices] IProductCategoryCommandService commandService,
        [FromServices] IProductCategoryQueryFactory queryFactory,
        [FromServices] IProductCategoryQueryService queryService
    ) {
        if (ModelState.IsValid)
        {
            CreateProductCategoryCommand command = commandFactory.Create(
                name: requestDto.Name,
                description: requestDto.Description,
                photo: requestDto.Photo
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

        return View(requestDto);
    }

    [Route("admin/categories/edit/{id}")]
    public async Task<IActionResult> Edit(
        int id,
        [FromServices] IProductCategoryQueryFactory queryFactory,
        [FromServices] IProductCategoryQueryService queryService
    ) {
        ProductCategory category = await queryService.GetByIdAsync(id);

        return View(new UpdateProductCategoryRequestDto(
            Id: category.Id,
            Name: category.Name,
            Description: category.Description,
            Photo: category.Photo
        ));
    }

    [HttpPost]
    [Route("admin/categories/edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        UpdateProductCategoryRequestDto requestDto,
        [FromServices] IUpdateProductCategoryCommandFactory commandFactory,
        [FromServices] IProductCategoryCommandService commandService
    ) {
        if (ModelState.IsValid)
        {
            UpdateProductCategoryCommand command = commandFactory.Create(
                id: requestDto.Id,
                name: requestDto.Name,
                description: requestDto.Description,
                photo: requestDto.Photo
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

        return View(requestDto);
    }

    [Route("admin/categories/delete/{id}")]
    public async Task<IActionResult> Delete(
        int id,
        [FromServices] IProductCategoryQueryService queryService
    ) {
        ProductCategory category = await queryService.GetByIdAsync(id);

        return View(category);
    }

    [HttpPost]
    [Route("admin/categories/delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(
        int id,
        [FromServices] IProductCategoryCommandService commandService
    ) {
        await commandService.DeleteByIdAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
