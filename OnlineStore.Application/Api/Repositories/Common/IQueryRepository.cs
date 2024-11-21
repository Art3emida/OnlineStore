namespace OnlineStore.Application.Api.Repositories.Common;

using System.Linq.Expressions;
using OnlineStore.Application.Query.Common;

public interface IQueryRepository<TEntity, TKeyType> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(
        TKeyType id,
        List<Expression<Func<TEntity, object>>>? includes = null
    );
    Task<IListStructure<TEntity>> FindAllAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        int? limit = null, 
        int? offset = null, 
        List<(Expression<Func<TEntity, object>> Column, bool IsDescending)>? sort = null,
        List<Expression<Func<TEntity, object>>>? includes = null
    );
    Task<TEntity?> FindOneAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        List<(Expression<Func<TEntity, object>> Column, bool IsDescending)>? sort = null,
        List<Expression<Func<TEntity, object>>>? includes = null
    );
    Task<int> GetCountAsync(
        Expression<Func<TEntity, bool>>? predicate = null
    );
}
