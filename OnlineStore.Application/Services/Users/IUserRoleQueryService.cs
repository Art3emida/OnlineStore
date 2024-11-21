namespace OnlineStore.Application.Services.Users;

using OnlineStore.Domain.Model.Users;

public interface IUserRoleQueryService
{
    Task<bool> ExistsAsync(RoleType roleType);
}
