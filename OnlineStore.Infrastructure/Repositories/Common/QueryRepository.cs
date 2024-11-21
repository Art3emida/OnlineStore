namespace OnlineStore.Infrastructure.Repositories.Common;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Api.Repositories.Common;
using OnlineStore.Application.DtoFactory.Common;
using OnlineStore.Application.Query.Common;
using OnlineStore.Infrastructure.Persistence.Context;

public abstract class QueryRepository<TEntity, TKeyType> : IQueryRepository<TEntity, TKeyType> where TEntity : class
{
    protected readonly MasterDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;
    protected readonly IListStructureFactory<TEntity> _listStructureFactory;

    public QueryRepository(
        MasterDbContext dbContext,
        IListStructureFactory<TEntity> listStructureFactory
    ) {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
        _listStructureFactory = listStructureFactory;
    }

    public async Task<TEntity?> GetByIdAsync(
        TKeyType id,
        List<Expression<Func<TEntity, object>>>? includes = null
    ) {
        IQueryable<TEntity> query = _dbSet;

        if (includes != null && includes.Any())
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync(e => EF.Property<TKeyType>(e, "Id").Equals(id));
    }

    public async Task<IListStructure<TEntity>> FindAllAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        int? limit = null, 
        int? offset = null, 
        List<(Expression<Func<TEntity, object>> Column, bool IsDescending)>? sort = null,
        List<Expression<Func<TEntity, object>>>? includes = null
    ) {
        IQueryable<TEntity> query = _dbSet;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (includes != null && includes.Any())
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (sort != null && sort.Any())
        {
            var firstSort = sort.First();
            query = firstSort.IsDescending
                ? query.OrderByDescending(firstSort.Column)
                : query.OrderBy(firstSort.Column);

            foreach (var sortOption in sort.Skip(1))
            {
                query = sortOption.IsDescending
                    ? ((IOrderedQueryable<TEntity>)query).ThenByDescending(sortOption.Column)
                    : ((IOrderedQueryable<TEntity>)query).ThenBy(sortOption.Column);
            }
        }

        if (offset.HasValue)
        {
            query = query.Skip(offset.Value);
        }

        if (limit.HasValue)
        {
            query = query.Take(limit.Value);
        }

        IEnumerable<TEntity> data = await query.ToListAsync();
        int total = await GetCountAsync(predicate);
        
        if (total == 0)
        {
            return _listStructureFactory.Create(
                data: data,
                total: 0,
                pageCount: 0,
                currentPage: 0,
                perPage: limit ?? 0
            );
        }
        
        int perPage = limit ?? total;
        int pageCount = total / perPage + (total % perPage > 0 ? 1 : 0);
        int currentPage = (offset ?? 0) / perPage + 1;

        return _listStructureFactory.Create(
            data: data,
            total: total,
            pageCount: pageCount,
            currentPage: currentPage,
            perPage: perPage
        );
    }

    public async Task<TEntity?> FindOneAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        List<(Expression<Func<TEntity, object>> Column, bool IsDescending)>? sort = null,
        List<Expression<Func<TEntity, object>>>? includes = null
    ) {
        IQueryable<TEntity> query = _dbSet;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (includes != null && includes.Any())
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (sort != null && sort.Any())
        {
            var firstSort = sort.First();
            query = firstSort.IsDescending
                ? query.OrderByDescending(firstSort.Column)
                : query.OrderBy(firstSort.Column);

            foreach (var sortOption in sort.Skip(1))
            {
                query = sortOption.IsDescending
                    ? ((IOrderedQueryable<TEntity>)query).ThenByDescending(sortOption.Column)
                    : ((IOrderedQueryable<TEntity>)query).ThenBy(sortOption.Column);
            }
        }

        return await query.FirstOrDefaultAsync();
    }
    
    public async Task<int> GetCountAsync(
        Expression<Func<TEntity, bool>>? predicate = null
    ) {
        IQueryable<TEntity> query = _dbSet;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.CountAsync();
    }
}
