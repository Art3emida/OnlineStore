namespace OnlineStore.Infrastructure.Services.Common;

using OnlineStore.Application.Api.Services.Common;
using OnlineStore.Infrastructure.Persistence.Context;

public class TransactionOperationService : ITransactionOperationService
{
    private readonly MasterDbContext _dbContext;

    public TransactionOperationService(MasterDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ExecuteAsTransactionAsync(Func<Task> operation)
    {
        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                await operation();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();

                throw;
            }
        }
    }
}
