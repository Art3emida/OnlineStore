namespace OnlineStore.Application.Services.Users;

using OnlineStore.Domain.Model.Users;

public interface IUserQueryService
{
    Task<User> GetUserByIdAsync(int id);
}
