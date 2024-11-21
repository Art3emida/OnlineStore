namespace OnlineStore.Application.Services.Products;

using OnlineStore.Application.Api.Repositories.Products;
using OnlineStore.Application.Api.Services.Common;
using OnlineStore.Application.ConstantBag.Products;
using OnlineStore.Application.Exceptions.Products;
using OnlineStore.Application.Query.Common;
using OnlineStore.Application.Query.Products;
using OnlineStore.Application.Services.Common;
using OnlineStore.Domain.Model.Products;

public class ProductQueryService : QueryService, IProductQueryService
{
    private readonly IProductQueryRepository _queryRepository;

    public ProductQueryService(
        IValidationService validationService,
        IProductQueryRepository queryRepository
    ) : base(
        validationService
    ) {
        _queryRepository = queryRepository;
    }

    public async Task<Product> GetByIdAsync(int id, ProductInclude include = ProductInclude.None)
    {
        Product? product = await _queryRepository.GetByIdAsync(
            id,
            include: include
        );

        if (product == null)
        {
            throw new ProductNotFoundException(string.Format(
                ProductExceptionBag.ProductNotFound,
                id
            ));
        }
        
        return product;
    }

    public async Task<IListStructure<Product>> FindAllByQueryAsync(IProductQuery query)
    {
        await ValidateQueryAsync(query);
        return await _queryRepository.FindAllByQueryAsync(query);
    }
}
