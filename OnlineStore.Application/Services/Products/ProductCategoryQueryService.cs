namespace OnlineStore.Application.Services.Products;

using OnlineStore.Application.Api.Repositories.Products;
using OnlineStore.Application.Api.Services.Common;
using OnlineStore.Application.ConstantBag.Products;
using OnlineStore.Application.Exceptions.Products;
using OnlineStore.Application.Query.Common;
using OnlineStore.Application.Query.Products;
using OnlineStore.Application.Services.Common;
using OnlineStore.Domain.Model.Products;

public class ProductCategoryQueryService : QueryService, IProductCategoryQueryService
{
    private readonly IProductCategoryQueryRepository _queryRepository;

    public ProductCategoryQueryService(
        IValidationService validationService,
        IProductCategoryQueryRepository queryRepository
    ) : base(
        validationService
    ) {
        _queryRepository = queryRepository;
    }

    public async Task<ProductCategory> GetByIdAsync(int id, ProductCategoryInclude include = ProductCategoryInclude.None)
    {
        ProductCategory? category = await _queryRepository.GetByIdAsync(
            id,
            include: include
        );

        if (category == null)
        {
            throw new ProductCategoryNotFoundException(string.Format(
                ProductCategoryExceptionBag.ProductCategoryNotFound,
                id
            ));
        }
        
        return category;
    }

    public async Task<IListStructure<ProductCategory>> FindAllByQueryAsync(IProductCategoryQuery query)
    {
        await ValidateQueryAsync(query);
        return await _queryRepository.FindAllByQueryAsync(query);
    }
}
