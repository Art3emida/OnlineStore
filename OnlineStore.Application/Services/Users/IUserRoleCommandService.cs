namespace OnlineStore.Application.Services.Users;

using OnlineStore.Domain.Model.Users;

public interface IUserRoleCommandService
{
    Task CreateAsync(RoleType roleType);
}
