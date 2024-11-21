namespace OnlineStore.Application.Api.Repositories.Common;

public interface ICommandRepository<TEntity, TKeyType> where TEntity : class
{
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task DeleteByIdAsync(TKeyType id);
}
