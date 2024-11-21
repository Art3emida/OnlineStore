namespace OnlineStore.Infrastructure.Repositories.Products;

using LinqKit;
using System.Linq.Expressions;
using OnlineStore.Application.Api.Repositories.Products;
using OnlineStore.Application.DtoFactory.Common;
using OnlineStore.Application.Query.Common;
using OnlineStore.Application.Query.Products;
using OnlineStore.Domain.Model.Products;
using OnlineStore.Infrastructure.Persistence.Context;
using OnlineStore.Infrastructure.Repositories.Common;

public class ProductQueryRepository : QueryRepository<Product, int>, IProductQueryRepository
{
    public ProductQueryRepository(
        MasterDbContext dbContext,
        IListStructureFactory<Product> listStructureFactory
    ) : base(
        dbContext,
        listStructureFactory
    ) {}

    public async Task<Product?> GetByIdAsync(int id, ProductInclude include = ProductInclude.None)
    {
        return await base.GetByIdAsync(
            id,
            includes: BuildIncludes(include)
        );
    }

    public async Task<IListStructure<Product>> FindAllByQueryAsync(IProductQuery query)
    {
        var predicate = PredicateBuilder.New<Product>(p => true);

        if (query.CategoryId != null)
        {
            predicate = predicate.And(p => p.CategoryId == query.CategoryId);
        }

        return await FindAllAsync(
            predicate: predicate,
            limit: query.PaginationControl?.Limit,
            offset: query.PaginationControl?.Offset,
            sort: BuildSort(query.Sort),
            includes: BuildIncludes(query.Include)
        );
    }

    private List<Expression<Func<Product, object>>> BuildIncludes(ProductInclude includeFlags)
    {
        var includes = new List<Expression<Func<Product, object>>>();

        if (includeFlags.HasFlag(ProductInclude.Photos))
        {
            includes.Add(p => p.Photos);
        }

        if (includeFlags.HasFlag(ProductInclude.Category))
        {
            includes.Add(p => p.Category);
        }

        return includes;
    }

    private List<(Expression<Func<Product, object>>, bool)> BuildSort(ProductSort sortFlags)
    {
        var sort = new List<(Expression<Func<Product, object>>, bool)>();

        if (sortFlags.HasFlag(ProductSort.NameAsc))
        {
            sort.Add((p => p.Name, false));
        }
        else if (sortFlags.HasFlag(ProductSort.NameDesc))
        {
            sort.Add((p => p.Name, true));
        }

        if (sortFlags.HasFlag(ProductSort.PriceAsc))
        {
            sort.Add((p => p.Price, false));
        }
        else if (sortFlags.HasFlag(ProductSort.PriceDesc))
        {
            sort.Add((p => p.Price, true));
        }

        if (sortFlags.HasFlag(ProductSort.CreatedAtAsc))
        {
            sort.Add((p => p.CreatedAt, false));
        }
        else if (sortFlags.HasFlag(ProductSort.CreatedAtDesc))
        {
            sort.Add((p => p.CreatedAt, true));
        }

        if (sortFlags.HasFlag(ProductSort.UpdatedAtAsc))
        {
            sort.Add((p => p.UpdatedAt ?? DateTime.MaxValue, false));
        }
        else if (sortFlags.HasFlag(ProductSort.UpdatedAtDesc))
        {
            sort.Add((p => p.UpdatedAt ?? DateTime.MinValue, true));
        }

        return sort;
    }
}
