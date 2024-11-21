namespace OnlineStore.Application.Api.Services.Common;

public interface ITransactionOperationService
{
    Task ExecuteAsTransactionAsync(Func<Task> operation);
}
