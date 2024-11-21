namespace OnlineStore.Infrastructure.Repositories.Products;

using System.Linq.Expressions;
using OnlineStore.Application.Api.Repositories.Products;
using OnlineStore.Application.DtoFactory.Common;
using OnlineStore.Application.Query.Common;
using OnlineStore.Application.Query.Products;
using OnlineStore.Domain.Model.Products;
using OnlineStore.Infrastructure.Persistence.Context;
using OnlineStore.Infrastructure.Repositories.Common;

public class ProductCategoryQueryRepository : QueryRepository<ProductCategory, int>, IProductCategoryQueryRepository
{
    public ProductCategoryQueryRepository(
        MasterDbContext dbContext,
        IListStructureFactory<ProductCategory> listStructureFactory
    ) : base(
        dbContext,
        listStructureFactory
    ) {}

    public async Task<ProductCategory?> GetByIdAsync(int id, ProductCategoryInclude include = ProductCategoryInclude.None)
    {
        return await base.GetByIdAsync(
            id,
            includes: BuildIncludes(include)
        );
    }

    public async Task<IListStructure<ProductCategory>> FindAllByQueryAsync(IProductCategoryQuery query)
    {
        return await FindAllAsync(
            limit: query.PaginationControl?.Limit,
            offset: query.PaginationControl?.Offset,
            sort: BuildSort(query.Sort),
            includes: BuildIncludes(query.Include)
        );
    }

    private List<Expression<Func<ProductCategory, object>>> BuildIncludes(ProductCategoryInclude includeFlags)
    {
        var includes = new List<Expression<Func<ProductCategory, object>>>();

        if (includeFlags.HasFlag(ProductCategoryInclude.Products))
        {
            includes.Add(c => c.Products);
        }

        return includes;
    }

    private List<(Expression<Func<ProductCategory, object>>, bool)> BuildSort(ProductCategorySort sortFlags)
    {
        var sort = new List<(Expression<Func<ProductCategory, object>>, bool)>();

        if (sortFlags.HasFlag(ProductCategorySort.NameAsc))
        {
            sort.Add((p => p.Name, false));
        }
        else if (sortFlags.HasFlag(ProductCategorySort.NameDesc))
        {
            sort.Add((p => p.Name, true));
        }

        if (sortFlags.HasFlag(ProductCategorySort.CreatedAtAsc))
        {
            sort.Add((p => p.CreatedAt, false));
        }
        else if (sortFlags.HasFlag(ProductCategorySort.CreatedAtDesc))
        {
            sort.Add((p => p.CreatedAt, true));
        }

        if (sortFlags.HasFlag(ProductCategorySort.UpdatedAtAsc))
        {
            sort.Add((p => p.UpdatedAt ?? DateTime.MaxValue, false));
        }
        else if (sortFlags.HasFlag(ProductCategorySort.UpdatedAtDesc))
        {
            sort.Add((p => p.UpdatedAt ?? DateTime.MinValue, true));
        }

        return sort;
    }
}
