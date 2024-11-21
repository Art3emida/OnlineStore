namespace OnlineStore.Infrastructure.Persistence.Initializers;

public interface IDbInitializer
{
    Task InitializeAsync();
}
