namespace OnlineStore.Infrastructure.Repositories.Common;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Api.Repositories.Common;
using OnlineStore.Infrastructure.Persistence.Context;

public abstract class CommandRepository<TEntity, TKeyType> : ICommandRepository<TEntity, TKeyType> where TEntity : class
{
    protected readonly MasterDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public CommandRepository(MasterDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task CreateAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(TKeyType id)
    {
        TEntity? entity = await _dbSet.FindAsync(id);

        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
